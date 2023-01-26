using System;
using System.Threading.Tasks;

namespace MoneyKeeper.Services.Mail
{
    public interface IMailService
    {
        Task SendEmail(string email, string subject, string body);
        public Task<String> SendRegisterMail(string email);
        public Task<String> SendResetPasswordMail(string email);
    }
}