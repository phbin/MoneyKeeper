using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Attributes;
using MoneyKeeper.Data;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MoneyKeeper.Dtos.Statistic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static MoneyKeeper.Common.Enum;
using MoneyKeeper.Dtos.Category;

namespace MoneyKeeper.Controllers.Wallets
{
    [ApiController]
    [Route("api/wallets/{id}/statistic")]
    [Protect]
    public class StatisticWalletController : Controller
    {
        public DataContext _context { get; set; }
        public IMapper _mapper { get; set; }

        public StatisticWalletController(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("group")]
        public async Task<IActionResult> GetReportGroupByDate(int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
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
            var transQuery = _context.Transactions.AsNoTracking().Where(t => t.WalletId == id && t.CreatedAt >= startDate && t.CreatedAt <= endDate);

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
        public async Task<IActionResult> GetSpendingReport(int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, bool showTopSpending = true)
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
            var transQuery = _context.Transactions.AsNoTracking().Where(t => t.WalletId == id && t.Category!.Type == CategoryType.Expense && t.CreatedAt >= startDate && t.CreatedAt <= endDate);

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
        public async Task<IActionResult> GetIncomeReport(int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, bool showTopIncome = true)
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
            var transQuery = _context.Transactions.AsNoTracking().Where(t => t.WalletId == id && t.Category!.Type == CategoryType.Income && t.CreatedAt >= startDate && t.CreatedAt <= endDate);

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
