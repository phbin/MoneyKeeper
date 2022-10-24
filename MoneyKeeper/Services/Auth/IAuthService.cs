using MoneyKeeper.Data.Users;
using MoneyKeeper.Models;
using System.Threading.Tasks;
using Users = MoneyKeeper.Models.Users;

namespace MoneyKeeper.Services.Auth
{
    public interface IAuthService
    {
        //string CreateToken(User user);
        //string ValidateToken(string token);
        public Task<string> SignIn(SignIn user);
        public Task<(Users,string)> SignUp(SignUp user);

        public string SendOTP(string email);

        public Task<(Users,string)> VerifyAccountSignUp(OneTimePassword code);
        //public Task ForgotPassword(string emai);
        //public Task ResetPassword(string emai);

        //public Task<bool> FindUserByEmai(string email);
        //public Task<(User, string)> ResetPassword(TokenResetPasswordDto UserDto);
        //public string VerifyResetPassword(string email, string value);
        //public Task ChangePassword(ChangePassword changePassword);
    }
}
