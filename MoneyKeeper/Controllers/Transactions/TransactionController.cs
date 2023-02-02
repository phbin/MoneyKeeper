using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Attributes;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos.Transaction;
using MoneyKeeper.Models;
using MoneyKeeper.Services.Category;
using MoneyKeeper.Services.Transactions;
using MoneyKeeper.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace MoneyKeeper.Controllers.Transactions
{
    [ApiController]
    [Route("api/wallets/{walletId}/transactions")]
    [Protect]
    public class TransactionController : Controller
    {
        public ITransactionService _transactionService { get; set; }
        public ICategoryService _categoryService { get; set; }
        public IWalletService _walletService { get; set; }
        public IMapper _mapper { get; set; }
        public TransactionController(ITransactionService transactionService, IMapper mapper, IWalletService walletService, ICategoryService categoryService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _walletService = walletService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(int walletId, [FromQuery] TransactionFilterDto filter)
        {
            int userId = HttpContext.Items["UserId"] as int? ?? 0;
            if (!await _walletService.VerifyIsUserInWallet(walletId, (int)userId!))
            {
                throw new ApiException("Access denied!", 400);
            }
            var transList = await _transactionService.GetTransactions(userId, walletId, filter);
            var transRes = _mapper.Map<IEnumerable<TransactionDto>>(transList);
            var groupByDates = transRes.GroupBy(tr => tr.CreatedAt.Date);
            List<TransactionGroupDateDto> response = new List<TransactionGroupDateDto>();
            long totalIncome = 0, totalExpense = 0;
            foreach (var group in groupByDates)
            {
                TransactionGroupDateDto item = new TransactionGroupDateDto { Date = group.Key };
                long revenue = 0;

                foreach (var transaction in group)
                {
                    if (transaction.Category!.Type == Common.Enum.CategoryType.Income)
                    {
                        totalIncome += transaction.Amount;
                        revenue += transaction.Amount;
                    }
                    else
                    {
                        totalExpense += transaction.Amount;
                        revenue -= transaction.Amount;
                    }
                    item.Transactions.Add(transaction);
                }

                item.Revenue = revenue;
                response.Add(item);
            }

            return Ok(new ApiResponse<object>(new
            {
                totalIncome,
                totalExpense,
                details = response
            }, "Get transaction statistic group by date successfully!"));
        }

        [HttpGet("recently")]
        [Produces(typeof(IEnumerable<TransactionDto>))]
        public async Task<IActionResult> GetRecentlyTransactions(int walletId, [FromQuery] TransactionFilterDto filter)
        {
            int userId = HttpContext.Items["UserId"] as int? ?? 0;
            if (!await _walletService.VerifyIsUserInWallet(walletId, (int)userId!))
            {
                throw new ApiException("Access denied!", 400);
            }

            filter.Skip = 0;
            filter.Take = 5;
            var transList = await _transactionService.GetTransactions(userId, walletId, filter);
            var transRes = _mapper.Map<IEnumerable<TransactionDto>>(transList);
            return Ok(new ApiResponse<object>(transRes, "Get recently transactions successfully!"));
        }

        [HttpGet("{id}")]
        [Produces(typeof(TransactionDto))]
        public async Task<IActionResult> GetTransactionById(int id, int walletId)
        {
            int userId = HttpContext.Items["UserId"] as int? ?? 0;
            if (!await _walletService.VerifyIsUserInWallet(walletId, (int)userId!))
            {
                throw new ApiException("Access denied!", 400);
            }

            var trans = await _transactionService.GetTransactionById(id);
            var transRes = _mapper.Map<TransactionDto>(trans);
            return Ok(new ApiResponse<object>(transRes, "Get recently transactions successfully!"));
        }

        [HttpPost]
        [Produces(typeof(ApiResponse<TransactionDto>))]
        public async Task<IActionResult> CreateTransaction(int walletId, [FromBody] CreateTransactionDto createTransactionDto)
        {
            var user = HttpContext.Items["User"] as User;
            var userId = HttpContext.Items["UserId"] as int?;
            if (!await _walletService.VerifyIsUserInWallet(walletId, (int)userId))
            {
                throw new ApiException("Access denied!", 400);
            }
            if (!await _categoryService.VerifyIsCategoryOfWallet(createTransactionDto.CategoryId, walletId))
            {
                throw new ApiException("Invalid category!", 400);
            }

            var trans = await _transactionService.CreateTransaction((int)userId, walletId, createTransactionDto);

            var transRes = _mapper.Map<TransactionDto>(trans);
            return new CreatedResult("", new ApiResponse<TransactionDto>(transRes, "Create successfully"));
        }

        [HttpPut("{id}")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> UpdateTransaction(int id, int walletId, [FromBody] UpdateTransactionDto updateTransDto)
        {
            var userId = Convert.ToInt32(HttpContext.Items["UserId"] as int?);

            if (!await _walletService.VerifyIsUserInWallet(walletId, (int)userId))
            {
                throw new ApiException("Access denied!", 400);
            }

            if (!await _categoryService.VerifyIsCategoryOfWallet((int)updateTransDto.CategoryId, walletId))
            {
                throw new ApiException("Invalid category!", 400);
            }

            await _transactionService.UpdateTransaction(id, walletId, updateTransDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> DeleteTrans(int id, int walletId)
        {
            var userId = HttpContext.Items["UserId"] as int?;

            if (!await _walletService.VerifyIsUserInWallet(walletId, (int)userId))
            {
                throw new ApiException("Access denied!", 400);
            }

            await _transactionService.DeleteTransaction(id);
            return NoContent();
        }
    }
}
