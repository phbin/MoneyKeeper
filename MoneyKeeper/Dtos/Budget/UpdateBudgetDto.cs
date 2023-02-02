using MoneyKeeper.Dtos.Category;

namespace MoneyKeeper.Dtos.Budget
{
    public class UpdateBudgetDto
    {
        public UpdateBudgetDto()
        {
        }
        public int LimitAmount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public int WalletId { get; set; }
    }
}
