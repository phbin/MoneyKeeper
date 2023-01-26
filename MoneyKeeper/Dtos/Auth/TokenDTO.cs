using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos.Auth
{
    public class TokenDTO
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
