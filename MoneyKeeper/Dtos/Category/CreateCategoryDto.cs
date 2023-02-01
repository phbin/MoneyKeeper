using static MoneyKeeper.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Icon is required")]
        public CategoryType Type { get; set; }
        public CategoryGroup Group { get; set; }
        public string Icon { get; set; }
        public int? WalletId { get; set; }
        public CreateCategoryDto()
        {
            Name = "";
            Icon = "https://picsum.photos/200";
        }
    }
}
