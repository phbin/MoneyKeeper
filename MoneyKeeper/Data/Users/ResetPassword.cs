using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;

namespace MoneyKeeper.Data.Users
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "The email address is required")]
        public string email { get; set; }
        public string newPassword { get; set; }
        public string retypePassword { get; set; }

        public ResetPassword()
        {
            email = string.Empty;
            newPassword = string.Empty;
            retypePassword = string.Empty;
        }
    }
}
