using MoneyKeeper.Data.Users;
using MoneyKeeper.Models;
using System.Threading.Tasks;
using Users = MoneyKeeper.Models.Users;

namespace MoneyKeeper.Services.Auth
{
    public interface IAuthService
    {
        public Task<string> SignIn(SignIn user);
        public Task<(Users,string)> SignUp(SignUp user);

        public string SendOTP(string email);

        public Task<(Users,string)> VerifyAccountSignUp(OneTimePassword code);
        public Task<string> ForgotPassword(string emai);
        public Task<string> VerifyResetPassword(OneTimePassword code);
        public Task<string> ResetPassword(ResetPassword code);
    }
}
