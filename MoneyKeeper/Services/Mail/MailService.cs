using MoneyKeeper.Services.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MoneyKeeper.Services.Mail
{
    public class MailService : IMailService
    {
        ICredentialsByHost _credentialsByHost;
        public MailService()
        {
            _credentialsByHost = new NetworkCredential("renyuiko.hamyana@gmail.com", "nawakyvfqncmjdyn"); 
        }
        public async Task<String> SendRegisterMail(string email)
        {
            string code = new Random().Next(1000, 9999).ToString();
            await Task.Run(() =>
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("renyuiko.hamyana@gmail.com");
                mail.Subject = "MoneyKeeperApp - Verification mail";
                mail.Body = "This is your verification code: " + code;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = _credentialsByHost;
                smtp.Send(mail);
            });
            return code;
        }

        public async Task<String> SendResetPasswordMail(string email)
        {
            string code = new Random().Next(1000, 9999).ToString();
            await Task.Run(() =>
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("renyuiko.hamyana@gmail.com");
                mail.Subject = "MoneyKeeperApp - Account recovery code mail";
                mail.Body = "Enter the following password reset code: " + code;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = _credentialsByHost;
                smtp.Send(mail);
            });
            return code;
        }

        public async Task SendEmail(string email, string subject, string body)
        {
            string code = new Random().Next(1000, 9999).ToString();
            await Task.Run(() =>
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("renyuiko.hamyana@gmail.com");
                mail.Subject = subject;
                mail.Body = body;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = _credentialsByHost;
                smtp.Send(mail);
            });
        }
    }
}
