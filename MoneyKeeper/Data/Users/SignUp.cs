using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Data.Users
{
    public class SignUp
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }
        [Required, MinLength(6, ErrorMessage = "Password is required and must be at least 6 character")]
        public string password { get; set; }
        public SignUp()
        {
            email = string.Empty;
            password = string.Empty;
        }
    }
}
