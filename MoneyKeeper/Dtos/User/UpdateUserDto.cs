using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos.User
{
    public class UpdateUserDto
    {
        [Required]
        public string Avatar { get; set; }
        public UpdateUserDto()
        {
        }
    }
}
