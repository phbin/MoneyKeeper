using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Data.Users
{
    public class OneTimePassword
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }
        public string otp { get; set; }
        public OneTimePassword()
        {
            email = string.Empty;
            otp = string.Empty;
        }
    }
}
