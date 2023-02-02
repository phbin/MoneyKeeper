using MoneyKeeper.Dtos.Category;

namespace MoneyKeeper.Dtos.Statistic
{
    public class CategoryStat
    {
        public CategoryDto Category { get; set; } = null!;
        public long Amount { get; set; }
    }
}
