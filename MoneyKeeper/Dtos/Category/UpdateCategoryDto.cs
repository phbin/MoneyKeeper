using static MoneyKeeper.Common.Enum;

namespace MoneyKeeper.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public string? Name { get; set; }
        public CategoryType? Type { get; set; }
        public CategoryGroup? Group { get; set; }
        public string? Icon { get; set; }
    }
}
