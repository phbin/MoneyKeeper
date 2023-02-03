using MoneyKeeper.Dtos.Budget;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Services
{
    public interface IBudgetService
    {
        Task<Budget> CreateBudget(int userId, CreateBudgetDto createDto);
        Task DeleteBudget(int id, int walletId);
        Task<IEnumerable<Budget>> GetBudgets(int walletId, int month, int year);
        Task<Budget> GetBudgetById(int id, int walletId, int month, int year);
        Task<BudgetSummary> SummaryBudget(int walletId, int month, int year);
        Task UpdateBudget(int id, int walletId, UpdateBudgetDto updateDto);
        Task UpdateSpentAmount(int categoryId, int month, int year, int amount, bool saveChanges = false);
        Task UpdateSpentAmount(int categoryId, int month, int year);
        Task<BudgetDetailSummary> SummaryBudgetDetail(int id, int walletId, int month, int year);
        Task<IEnumerable<BudgetDetailStatistic>> StatisticBudget(int id, int walletId, int month, int year);

    }
}
