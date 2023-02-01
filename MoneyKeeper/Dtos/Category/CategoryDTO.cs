using static MoneyKeeper.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Icon { get; set; }
        public CategoryType Type { get; set; }
        public CategoryGroup Group { get; set; }
        public int? WalletId { get; set; }
        public WalletDto? Wallet { get; set; }
        public CategoryDto()
        {
            Name = "";
            Icon = "https://picsum.photos/200";
            Wallet = null;
        }
    }
}
