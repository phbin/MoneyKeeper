using MoneyKeeper.Dtos.Category;

namespace MoneyKeeper.Dtos.Budget
{
    public class BudgetDto
    {
        public BudgetDto()
        {
        }
        public int Id { get; set; }
        public int SpentAmount { get; set; }
        public int LimitAmount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public int WalletId { get; set; }
        public WalletDto? Wallet { get; set; }
        public int CreatorId { get; set; }
        public UserDto? Creator { get; set; }
    }
}
