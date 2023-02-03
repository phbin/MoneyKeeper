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
using MoneyKeeper.Dtos.Transaction;
using MoneyKeeper.Services.Event;
using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly IConfiguration _configuration;
        private readonly IBudgetService _budgetService;
        private readonly IWalletService _walletService;
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public DataContext _context { get; set; }

        public TransactionService(IConfiguration configuration, DataContext context, IMapper mapper, IBudgetService budgetService, IWalletService walletService, IEventService eventService)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _budgetService = budgetService;
            _walletService = walletService;
            _eventService = eventService;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(int userId, int? walletId, TransactionFilterDto filter)
        {
            var transQuery = _context.Transactions.AsQueryable();

            if (walletId.HasValue)
            {
                transQuery = transQuery.Where(t => t.WalletId == walletId);
            }
            else
            {
                transQuery = transQuery.Where(t => t.Wallet!.WalletMembers.Any(wM => wM.UserId == userId && wM.Status == Common.Enum.MemberStatus.Accepted));
            }

            transQuery = transQuery.Include(t => t.Category)
            .Include(t => t.Event)
            .OrderByDescending(t => t.CreatedAt)
            .AsNoTracking();

            if (filter.CategoryId.HasValue)
            {
                transQuery = transQuery.Where(t => t.CategoryId == filter.CategoryId);
            }

            if (filter.EventId.HasValue)
            {
                transQuery = transQuery.Where(t => t.EventId == filter.EventId);
            }

            if (filter.StartDate.HasValue)
            {
                var startDate = filter.StartDate?.Date;
                transQuery = transQuery.Where(t => t.CreatedAt >= startDate);
            }

            if (filter.EndDate.HasValue)
            {
                var EndDate = filter.EndDate?.Date.AddDays(1).AddSeconds(-1);
                transQuery = transQuery.Where(t => t.CreatedAt <= EndDate);
            }

            if (filter.Skip.HasValue)
            {
                transQuery = transQuery.Skip(filter.Skip.Value);
            }

            if (filter.Take.HasValue)
            {
                transQuery = transQuery.Take(filter.Take.Value);
            }

            var trans = await transQuery.ToListAsync();
            return trans;
        }

        public async Task<Transaction?> GetTransactionById(int id)
        {
            var transQuery = _context.Transactions.Where(t => t.Id == id)
            .Include(t => t.Category)
            .Include(t => t.Event)
            .Include(t => t.Creator)
            .Include(t => t.Wallet)
            .OrderByDescending(t => t.CreatedAt)
            .AsNoTracking();

            var trans = await transQuery.FirstOrDefaultAsync();
            if (trans == null)
            {
                return trans;
            }
            var participants = trans.ParticipantIds.Split(";", StringSplitOptions.RemoveEmptyEntries).Where(i => i.All(char.IsDigit)).Select(id => Convert.ToInt32(id))
            .ToList();

            if (participants.Count > 0)
            {
                trans.Participants = await _context.Users.AsNoTracking().Where(u => participants.Contains(u.Id)).ToListAsync();
            }

            return trans;
        }

        public async Task<Transaction> CreateTransaction(int userId, int walletId, CreateTransactionDto transDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var trans = _mapper.Map<Transaction>(transDto);

                trans.CreatorId = userId;
                trans.WalletId = walletId;

                if (trans.EventId.HasValue && !await _eventService.CheckEventIsInWallet((int)trans.EventId, trans.WalletId, trans.CreatorId))
                {
                    throw new ApiException("Invalid Event", 400);
                }

                int amount = transDto.Amount;
                var cate = await _context.Categories.Where(c => c.Id == trans.CategoryId).FirstOrDefaultAsync();
                await _budgetService.UpdateSpentAmount(trans.CategoryId, trans.CreatedAt.Month,
                    trans.CreatedAt.Year, trans.Amount, saveChanges: false);
                if (cate!.Type == Common.Enum.CategoryType.Expense)
                {
                    amount *= -1;
                }
                await _walletService.UpdateWalletBalance(walletId, amount);
                await _context.Transactions.AddAsync(trans);
                await _context.SaveChangesAsync();

                if (trans.EventId.HasValue)
                {
                    await _eventService.UpdateEventSpent((int)trans.EventId);
                }

                if (transDto.ParticipantIds.Count > 0)
                {
                    var memberIds = await _context.WalletMembers
                    .Where(wM => wM.WalletId == walletId && wM.Status == MemberStatus.Accepted)
                    .Select(wM => wM.UserId).ToListAsync();
                    var invalidUsers = transDto.ParticipantIds
                .Except(memberIds).Select(id => id).ToList();
                    if (invalidUsers.Count > 0)
                    {
                        throw new ApiException("Users " + string.Join(',', invalidUsers) + " not exist!", 400);
                    }
                    trans.ParticipantIds = String.Join(';', transDto.ParticipantIds.Where(id => id != trans.CreatorId).ToList());
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return trans;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateTransaction(int transactionId, int walletId, UpdateTransactionDto updateTransDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var trans = await _context.Transactions
                .Where(t => t.Id == transactionId && t.WalletId == walletId)
                .Include(t => t.Category)
                .FirstOrDefaultAsync();
                if (trans == null)
                {
                    throw new ApiException("Transaction not found!", 400);
                }


                if (trans.EventId.HasValue && !await _eventService.CheckEventIsInWallet((int)trans.EventId, trans.WalletId, trans.CreatorId))
                {
                    throw new ApiException("Invalid Event", 400);
                }

                if (updateTransDto.CategoryId != trans.CategoryId)
                {
                    await _budgetService.UpdateSpentAmount(trans.CategoryId, updateTransDto.CreateAtValue.Month,
                    updateTransDto.CreateAtValue.Year, updateTransDto.Amount, saveChanges: false);

                    await _budgetService.UpdateSpentAmount(trans.CategoryId, trans.CreatedAt.Month,
                    trans.CreatedAt.Year, -1 * trans.Amount, saveChanges: false);

                    var updatedCate = await _context.Categories.Where(c => c.Id == updateTransDto.CategoryId).FirstOrDefaultAsync();
                    int amount = trans.Amount;
                    int updatedAmount = updateTransDto.Amount;
                    if (updatedCate!.Type == Common.Enum.CategoryType.Expense)
                    {
                        updatedAmount *= -1;
                    }
                    if (trans.Category!.Type == Common.Enum.CategoryType.Income)
                    {
                        amount *= -1;
                    }
                    await _walletService.UpdateWalletBalance(walletId, amount + updatedAmount);
                }
                else if (updateTransDto.Amount != trans.Amount)
                {
                    await _budgetService.UpdateSpentAmount(trans.CategoryId, updateTransDto.CreateAtValue.Month,
            updateTransDto.CreateAtValue.Year, updateTransDto.Amount - trans.Amount, saveChanges: false);

                    int amount = updateTransDto.Amount - trans.Amount;
                    if (trans.Category!.Type == Common.Enum.CategoryType.Expense)
                    {
                        amount *= -1;
                    }
                    await _walletService.UpdateWalletBalance(walletId, amount);
                }
                _mapper.Map(updateTransDto, trans);


                var memberIds = await _context.WalletMembers
                .Where(wM => wM.WalletId == walletId)
                .Select(wM => wM.UserId).ToListAsync();
                var invalidUsers = updateTransDto.ParticipantIds
                .Except(memberIds).Select(id => id).ToList();
                if (invalidUsers.Count > 0)
                {
                    throw new ApiException("Users " + string.Join(',', invalidUsers) + " not exist!", 400);
                }
                trans.ParticipantIds = String.Join(';', updateTransDto.ParticipantIds.Where(id => id != trans.CreatorId));

                await _context.SaveChangesAsync();
                if (trans.EventId.HasValue || updateTransDto.EventId.HasValue)
                {
                    await _eventService.UpdateEventSpent((int)trans.EventId!);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteTransaction(int transactionId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var trans = await _context.Transactions.Where(t => t.Id == transactionId).Include(t => t.Category).FirstOrDefaultAsync();
                if (trans == null)
                {
                    throw new ApiException("Transaction not found!", 400);
                }

                if (trans.EventId.HasValue && !(await _eventService.CheckEventIsInWallet((int)trans.EventId, trans.WalletId, trans.CreatorId)))
                {
                    throw new ApiException("Invalid Event", 400);
                }

                await _budgetService.UpdateSpentAmount(trans.CategoryId, trans.CreatedAt.Month,
                    trans.CreatedAt.Year, -1 * trans.Amount, saveChanges: false);

                int amount = trans.Amount;
                if (trans.Category!.Type == Common.Enum.CategoryType.Income)
                {
                    amount *= -1;
                }
                await _walletService.UpdateWalletBalance(trans.WalletId, amount);

                _context.Transactions.Remove(trans);
                await _context.SaveChangesAsync();

                if (trans.EventId.HasValue)
                {
                    await _eventService.UpdateEventSpent((int)trans.EventId!);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
