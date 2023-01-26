using System;

namespace MoneyKeeper.Dtos.Auth
{
    public class Token
    {
        public string Code { get; set; } = null!;
        public DateTime ExpiredAt;
        public RegisterUserDto User = null!;
    }
}
