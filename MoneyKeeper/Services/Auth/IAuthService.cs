﻿using MoneyKeeper.Dtos;
using MoneyKeeper.Dtos.Auth;
using MoneyKeeper.Dtos.User;
using System.Threading.Tasks;
using User = MoneyKeeper.Models.User;

namespace MoneyKeeper.Services.Auth
{
    public interface IAuthService
    {
        string CreateToken(User user);
        string ValidateToken(string token);
        public Task<(User, string)> Login(LoginUserDto UserDto);
        public Task Register(RegisterUserDto userDTO);
        public Task ForgotPassword(string emai);
        public Task<bool> FindUserByEmai(string email);
        public Task<(User, string)> VerifyEmailToken(TokenDTO tokenDTO);
        public Task<(User, string)> ResetPassword(TokenResetPasswordDto UserDto);
        public string VerifyResetPassword(string email, string value);
        public Task ChangePassword(ChangePassword changePassword);
    }
}
