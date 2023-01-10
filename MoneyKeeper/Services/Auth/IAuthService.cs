using MoneyKeeper.Data.Users;
using MoneyKeeper.Models;
using System.Threading.Tasks;
using User = MoneyKeeper.Models.User;

namespace MoneyKeeper.Services.Auth
{
    public interface IAuthService
    {
        public Task<(User,string)> SignIn(SignIn user);
        public Task<(User,string)> SignUp(SignUp user);

        public string SendOTP(string email);

        public Task<(User,string)> VerifyAccountSignUp(OneTimePassword code);
        public Task<string> ForgotPassword(string emai);
        public Task<string> VerifyResetPassword(OneTimePassword code);
        public Task<(User,string)> ResetPassword(ResetPassword code);
    }
}
