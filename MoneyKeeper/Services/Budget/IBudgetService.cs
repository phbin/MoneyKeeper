using MoneyKeeper.Dtos.Budget;
using System.Collections.Generic;
using System.Threading.Tasks;
using Budget = MoneyKeeper.Models.Budget;

namespace MoneyKeeper.Services.Budget
{
    public interface IBudgetService
    {
        Task<Models.Budget> CreateBudget(int userId, CreateBudgetDto createDto);
        Task DeleteBudget(int id, int walletId);
        Task<IEnumerable<Models.Budget>> GetBudgets(int walletId, int month, int year);
        Task<Models.Budget> GetBudgetById(int id, int walletId, int month, int year);
        Task<BudgetSummary> SummaryBudget(int walletId, int month, int year);
        Task UpdateBudget(int id, int walletId, UpdateBudgetDto updateDto);
        Task UpdateSpentAmount(int categoryId, int month, int year, int amount, bool saveChanges = false);
        Task UpdateSpentAmount(int categoryId, int month, int year);
        Task<BudgetDetailSummary> SummaryBudgetDetail(int id, int walletId, int month, int year);
        Task<IEnumerable<BudgetDetailStatistic>> StatisticBudget(int id, int walletId, int month, int year);

    }
}
