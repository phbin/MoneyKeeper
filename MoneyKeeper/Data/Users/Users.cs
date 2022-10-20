using System.ComponentModel.DataAnnotations;
using System;

namespace MoneyKeeper.Data.Users
{
    public class Users
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public string otp { get; set; }
        public Users()
        {
            email = string.Empty;
            otp = string.Empty;
        }
    }
}
