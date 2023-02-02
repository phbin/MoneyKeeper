using MoneyKeeper.Dtos;
using MoneyKeeper.Dtos.Auth;
using MoneyKeeper.Dtos.Transaction;
using MoneyKeeper.Dtos.User;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Services.Transactions
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransaction(int userId, int walletId, CreateTransactionDto transDto);
        Task DeleteTransaction(int transactionId);
        Task UpdateTransaction(int transactionId, int walletId, UpdateTransactionDto updateTransDto);
        Task<IEnumerable<Transaction>> GetTransactions(int userId, int? walletId, TransactionFilterDto filter);
        Task<Transaction?> GetTransactionById(int id);
    }
}
