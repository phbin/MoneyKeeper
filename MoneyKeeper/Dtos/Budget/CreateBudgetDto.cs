namespace MoneyKeeper.Dtos.Budget
{
    public class CreateBudgetDto
    {
        public CreateBudgetDto()
        {
        }
        public int LimitAmount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
        public int WalletId { get; set; }
    }
}
