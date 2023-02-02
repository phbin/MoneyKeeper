using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Attributes;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos.Statistic;
using MoneyKeeper.Dtos.Transaction;
using MoneyKeeper.Models;
using MoneyKeeper.Services.Category;
using MoneyKeeper.Services.Transactions;
using MoneyKeeper.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static MoneyKeeper.Common.Enum;
using MoneyKeeper.Dtos.Category;

namespace MoneyKeeper.Controllers
{
    [ApiController]
    [Route("api/global-wallets")]
    [Protect]
    public class GlobalWalletController : Controller
    {
        public ITransactionService _transactionService { get; set; }
        public ICategoryService _categoryService { get; set; }
        public IWalletService _walletService { get; set; }
        public DataContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public GlobalWalletController(ITransactionService transactionService, IMapper mapper, IWalletService walletService, ICategoryService categoryService, DataContext context)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _walletService = walletService;
            _categoryService = categoryService;
            _context = context;
        }

        [HttpGet("transactions/recently")]
        [Produces(typeof(IEnumerable<TransactionDto>))]
        public async Task<IActionResult> GetRecentlyTransactions([FromQuery] TransactionFilterDto filter)
        {
            int userId = HttpContext.Items["UserId"] as int? ?? 0;
            filter.Skip = 0;
            filter.Take = 5;
            var transList = await _transactionService.GetTransactions(userId, null, filter);
            var transRes = _mapper.Map<IEnumerable<TransactionDto>>(transList);
            return Ok(new ApiResponse<object>(transRes, "Get recently transactions successfully!"));
        }

        [HttpGet("group")]
        public async Task<IActionResult> GetReportGroupByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;
            startDate = startDate.Date;
            endDate = endDate.Date;
            if (startDate >= endDate)
            {
                throw new ApiException("End date must be greater than start date", 400);
            }

            int duration = endDate.Subtract(startDate).Days + 1;

            if (duration > 35)
            {
                throw new ApiException("End date can not be greater than start time 35 days", 400);
            }
            List<DailyReport> listDailyReport = new List<DailyReport>();
            endDate = endDate.AddDays(1).AddSeconds(-1);
            var transQuery = _context.Transactions.AsNoTracking().Where(t => t.Wallet!.WalletMembers.Any(wM => wM.UserId == userId && wM.Status == MemberStatus.Accepted)
            && t.CreatedAt >= startDate && t.CreatedAt <= endDate);

            var dailyReportMap = (await transQuery.GroupBy(t => t.CreatedAt.Date).Select(gr =>
            new DailyReport
            {
                Date = gr.Key,
                Expense = gr.Where(i => i.Category!.Type == CategoryType.Expense).Sum(t => t.Amount),
                Income = gr.Where(i => i.Category!.Type == CategoryType.Income).Sum(t => t.Amount),
            }).ToListAsync()).ToDictionary(r => r.Date, r => r);

            for (int i = 0; i < duration; i++)
            {
                DateTime date = startDate.AddDays(i);
                var report = dailyReportMap.ContainsKey(date) ? dailyReportMap[date] : new DailyReport { Date = date, Income = 0, Expense = 0 };
                listDailyReport.Add(report);
            }

            long netIncome = dailyReportMap.Values.Sum(t => t.Income - t.Expense);

            return Ok(new ApiResponse<object>(new
            {
                dailyReports = listDailyReport,
                netIncome = netIncome,
            }, "Get report of wallet successfully!"));
        }
        [HttpGet("expense")]
        public async Task<IActionResult> GetSpendingReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, bool showTopSpending = true)
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;
            startDate = startDate.Date;
            endDate = endDate.Date;
            if (startDate >= endDate)
            {
                throw new ApiException("End date must be greater than start date", 400);
            }

            int duration = endDate.Subtract(startDate).Days + 1;

            if (duration > 35)
            {
                throw new ApiException("End date can not be greater than start time 35 days", 400);
            }

            endDate = endDate.AddDays(1).AddSeconds(-1);
            var transQuery = _context.Transactions.AsNoTracking().Where(t => t.Wallet!.WalletMembers.Any(wM => wM.UserId == userId && wM.Status == MemberStatus.Accepted)
            && t.Category!.Type == CategoryType.Expense && t.CreatedAt >= startDate && t.CreatedAt <= endDate);

            var listTopSpending = new List<CategoryStat>();
            if (showTopSpending)
            {
                listTopSpending = (await transQuery.GroupBy(t => t.CategoryId).Select(gr =>
                new
                {
                    CategoryId = gr.Key,
                    Amount = gr.Sum(t => t.Amount),
                    Category = gr.FirstOrDefault()!.Category!,
                }
                ).ToListAsync()).Select(i => new CategoryStat
                {
                    Category = new CategoryDto
                    {
                        Id = i.Category.Id,
                        Icon = i.Category.Icon,
                        Name = i.Category.Name,
                        Type = i.Category.Type,
                        Group = i.Category.Group
                    },
                    Amount = i.Amount
                }).ToList();
            }

            long expense = await transQuery.SumAsync(t => t.Amount);

            return Ok(new ApiResponse<object>(new
            {
                details = listTopSpending,
                totalAmount = expense,
            }, "statistic wallet successfully!"));
        }

        [HttpGet("income")]
        public async Task<IActionResult> GetIncomeReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, bool showTopIncome = true)
        {
            var userId = (int)(HttpContext.Items["UserId"] as int?)!;
            startDate = startDate.Date;
            endDate = endDate.Date;
            if (startDate >= endDate)
            {
                throw new ApiException("End date must be greater than start date", 400);
            }

            int duration = endDate.Subtract(startDate).Days + 1;

            if (duration > 35)
            {
                throw new ApiException("End date can not be greater than start time 35 days", 400);
            }


            endDate = endDate.AddDays(1).AddSeconds(-1);
            var transQuery = _context.Transactions.AsNoTracking().Where(t => t.Wallet!.WalletMembers.Any(wM => wM.UserId == userId && wM.Status == MemberStatus.Accepted)
            && t.Category!.Type == CategoryType.Income && t.CreatedAt >= startDate && t.CreatedAt <= endDate);

            var incomes = new List<CategoryStat>();
            if (showTopIncome)
            {
                incomes = (await transQuery.GroupBy(t => t.CategoryId).Select(gr =>
                new
                {
                    CategoryId = gr.Key,
                    Amount = gr.Sum(t => t.Amount),
                    Category = gr.FirstOrDefault()!.Category!,
                }
                ).ToListAsync()).Select(i => new CategoryStat
                {
                    Category = new CategoryDto
                    {
                        Id = i.Category.Id,
                        Icon = i.Category.Icon,
                        Name = i.Category.Name,
                        Type = i.Category.Type,
                        Group = i.Category.Group
                    },
                    Amount = i.Amount
                }).ToList();
            }

            long expense = await transQuery.SumAsync(t => t.Amount);

            return Ok(new ApiResponse<object>(new
            {
                details = incomes,
                totalAmount = expense,
            }, "statistic wallet successfully!"));
        }
    }
}
