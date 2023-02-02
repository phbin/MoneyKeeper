using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyKeeper.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MoneyKeeper.Dtos.Auth;
using MoneyKeeper.Dtos.User;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos;
using Microsoft.AspNetCore.SignalR;
using MoneyKeeper.Dtos.Budget;
using MoneyKeeper.Helper;
using MoneyKeeper.Hubs;
using System.Linq.Expressions;

namespace MoneyKeeper.Services.Budget
{
    public class BudgetService : IBudgetService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        IHubContext<NotiHub> _notiHub;
        INotiService _notiService;

        public DataContext _context { get; set; }

        public BudgetService(IConfiguration configuration, DataContext context, IMapper mapper, IHubContext<NotiHub> notiHub, INotiService notiService)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _notiHub = notiHub;
            _notiService = notiService;
        }

        public async Task<IEnumerable<Models.Budget>> GetBudgets(int walletId, int month, int year)
        {
            var budgets = await _context.Budgets.AsNoTracking().Where(b => b.WalletId == walletId && b.Month == month && b.Year == year)
            .Include(b => b.Category)
            .ToListAsync();

            return budgets;
        }
        public async Task<Models.Budget> GetBudgetById(int id, int walletId, int month, int year)
        {
            var budget = await _context.Budgets.AsNoTracking().Where(b => b.WalletId == walletId && b.Month == month && b.Year == year)
            .Include(b => b.Category)
            .FirstOrDefaultAsync();

            return budget;
        }
        public async Task<BudgetSummary> SummaryBudget(int walletId, int month, int year)
        {
            var budgetsQuery = _context.Budgets.AsNoTracking().Where(b => b.WalletId == walletId && b.Month == month && b.Year == year);

            var budgetSum = new BudgetSummary
            {
                TotalBudget = await budgetsQuery.SumAsync(b => b.LimitAmount),
                TotalSpentAmount = await budgetsQuery.SumAsync(b => b.SpentAmount),
            };
            return budgetSum;
        }

        async Task<bool> IsBudgetExist(int categoryId, int month, int year, Expression<Func<Models.Budget, bool>>? predicate = null)
        {
            var budgetQuery = _context.Budgets.Where(b => b.CategoryId == categoryId && b.Month == month && b.Year == year);
            if (predicate != null)
            {
                budgetQuery = budgetQuery.Where(predicate);
            }
            return await budgetQuery.AnyAsync();
        }

        public async Task<Models.Budget> CreateBudget(int userId, CreateBudgetDto createDto)
        {
            var cate = await _context.Categories.Where(c => c.Id == createDto.CategoryId).FirstOrDefaultAsync();

            if (cate == null || cate.Type == Common.Enum.CategoryType.Income)
            {
                throw new ApiException("Invalid category", 400);
            }

            if (await IsBudgetExist(createDto.CategoryId, createDto.Month, createDto.Year))
            {
                throw new ApiException("Budget for this category in this time duration have already existed!", 400);
            }

            var budget = _mapper.Map<Models.Budget>(createDto);

            var spentInMonth = _context.Transactions.Where(t => t.CategoryId == budget.CategoryId
            && t.CreatedAt.Month == createDto.Month && t.CreatedAt.Year == createDto.Year).Sum(t => t.Amount);

            budget.CreatorId = userId;
            budget.SpentAmount = spentInMonth;
            await _context.Budgets.AddAsync(budget);

            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task UpdateBudget(int id, int walletId, UpdateBudgetDto updateDto)
        {
            var budget = await _context.Budgets.Where(t => t.Id == id && t.WalletId == walletId).FirstOrDefaultAsync();
            if (budget == null)
            {
                throw new ApiException("Budget not found!", 400);
            }

            if (updateDto.CategoryId != budget.CategoryId)
            {
                var cate = await _context.Categories.Where(c => c.Id == updateDto.CategoryId).FirstOrDefaultAsync();

                if (cate == null || cate.Type == Common.Enum.CategoryType.Income)
                {
                    throw new ApiException("Invalid category", 400);
                }

                if (await IsBudgetExist(updateDto.CategoryId, updateDto.Month, updateDto.Year))
                {
                    throw new ApiException("Budget for this category in this time duration have already existed!", 400);
                }

                var spentInMonth = _context.Transactions.Where(t => t.CategoryId == updateDto.CategoryId
                && t.CreatedAt.Month == updateDto.Month && t.CreatedAt.Year == updateDto.Year).Sum(t => t.Amount);
                budget.SpentAmount = spentInMonth;
            }


            _mapper.Map(updateDto, budget);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSpentAmount(int categoryId, int month, int year, int amount, bool saveChanges = false)
        {
            var budget = await _context.Budgets.Where(b => b.CategoryId == categoryId && b.Month == month && b.Year == year).FirstOrDefaultAsync();

            if (budget != null)
            {
                long beforeSpendAmout = budget.SpentAmount;
                budget.SpentAmount += amount;

                IEnumerable<Notification>? notisList = null;
                if (beforeSpendAmout <= budget.LimitAmount && budget.SpentAmount > budget.LimitAmount)
                {
                    var memberIds = await _context.WalletMembers
                    .Where(wM => wM.WalletId == budget.WalletId && wM.Status == Common.Enum.MemberStatus.Accepted)
                    .Select(wM => wM.UserId).ToArrayAsync();

                    await _context.Entry(budget).Reference(b => b.Category).LoadAsync();

                    var notisDto = memberIds.Select(id => new CreateNotificationDto
                    {
                        UserId = id,
                        BudgetId = budget.Id,
                        WalletId = budget.WalletId,
                        Type = Common.Enum.NotificationType.BudgetExceed,
                        Description = NotiTemplate.GetRemindBudgetExceedLimit(budget.Category?.Name ?? "", month, year),
                    });

                    notisList = await _notiService.CreateListNoti(notisDto, false);
                }

                if (saveChanges)
                {
                    await _context.SaveChangesAsync();
                }

                if (notisList != null)
                {
                    var notisDtos = _mapper.Map<IEnumerable<NotificationDto>>(notisList);
                    foreach (var noti in notisList)
                    {
                        _ = _notiHub.Clients.User(noti.UserId.ToString()).SendAsync("Notification", noti);
                    }
                }
            }
        }
        public async Task DeleteBudget(int id, int walletId)
        {
            var budget = await _context.Budgets.Where(t => t.Id == id && t.WalletId == walletId).FirstOrDefaultAsync();
            if (budget == null)
            {
                throw new ApiException("Transaction not found!", 400);
            }
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }

        public async Task<BudgetDetailSummary> SummaryBudgetDetail(int id, int walletId, int month, int year)
        {
            var budget = await _context.Budgets.Where(b => b.Id == id && b.WalletId == walletId && b.Month == month && b.Year == year)
             .FirstOrDefaultAsync();

            if (budget == null)
            {
                throw new ApiException("Invalid budget", 400);
            }

            DateTime now = DateTime.Now;
            DateTime date = new DateTime(year, month, 1);
            if (now.Year != year || now.Month != month)
            {
                var RealDailyExpense = budget.SpentAmount / DateTime.DaysInMonth(year, month);
                return new BudgetDetailSummary
                {
                    TotalBudget = budget.LimitAmount,
                    TotalSpentAmount = budget.SpentAmount,
                    ExpectedExpense = 0,
                    RealDailyExpense = RealDailyExpense
                };
            }


            int remainDaysOfMonth = DateTime.DaysInMonth(year, month) - DateTime.UtcNow.Day;
            double realDailyExpense = budget.SpentAmount * 1.0 / DateTime.UtcNow.Day;
            var summary = new BudgetDetailSummary
            {
                ExpectedExpense = realDailyExpense * remainDaysOfMonth + budget.SpentAmount,
                RealDailyExpense = realDailyExpense,
                TotalBudget = budget.LimitAmount,
                TotalSpentAmount = budget.SpentAmount,
                RecommendedDailyExpense = remainDaysOfMonth == 0 ? 0 : budget.RemainingAmount / remainDaysOfMonth,
            };

            return summary;
        }

        async Task<IEnumerable<BudgetDetailStatistic>> IBudgetService.StatisticBudget(int id, int walletId, int month, int year)
        {
            var budget = await _context.Budgets.Where(b => b.Id == id && b.WalletId == walletId && b.Month == month && b.Year == year)
             .FirstOrDefaultAsync();

            if (budget == null)
            {
                throw new ApiException("Invalid budget", 400);
            }

            int day = DateTime.DaysInMonth(year, month);

            //if (year == DateTime.Now.Year && month == DateTime.Now.Month)
            //{
            //    day = DateTime.Now.Day;
            //}

            List<long> amountEachDay = new List<long>(new long[day]);

            DateTime startDate = new DateTime(year, month, 1).ToUniversalTime();
            DateTime endDate = startDate.AddMonths(1).AddSeconds(-1).ToUniversalTime();
            var statistic = await _context.Transactions.Where(t => t.CategoryId == budget.CategoryId && t.CreatedAt >= startDate && t.CreatedAt <= endDate)
            .GroupBy(t => t.CreatedAt.Date)
            .Select(gr => new { Date = gr.Key, ExpenseAmount = gr.Sum(t => t.Amount) }).ToListAsync();

            foreach (var item in statistic)
            {
                amountEachDay[item.Date.Day - 1] += item.ExpenseAmount;
            }


            return amountEachDay.Select((v, i) => new BudgetDetailStatistic { Date = new DateTime(year, month, i + 1), ExpenseAmount = v }).ToList();
        }

        async Task IBudgetService.UpdateSpentAmount(int categoryId, int month, int year)
        {
            var budget = await _context.Budgets.Where(b => b.CategoryId == categoryId && b.Month == month && b.Year == year)
            .FirstOrDefaultAsync();

            if (budget == null)
            {
                throw new ApiException("Invalid budget", 400);
            }

            var spentInMonth = _context.Transactions.Where(t => t.CategoryId == budget.CategoryId
&& t.CreatedAt.Month == month && t.CreatedAt.Year == year).Sum(t => t.Amount);

            budget.SpentAmount = spentInMonth;
        }
    }
}
