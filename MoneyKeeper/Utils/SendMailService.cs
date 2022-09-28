using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper
{
    public class SendMailService
    {
        MailSettings _mailSettings = new MailSettings();
        public SendMailService()
        {
            _mailSettings.DisplayName = "MONEYKEEPER";
            _mailSettings.Mail = "renyuiko.hamyana@gmail.com";
            _mailSettings.Password = "nawakyvfqncmjdyn";
            _mailSettings.Port = 587;
            _mailSettings.Host = "smtp.gmail.com";
        }
        public async Task<string> SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(new MailboxAddress(mailContent.To, mailContent.To));
            email.Subject = mailContent.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try {
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return "Error" + e.Message;
            }
            smtp.Disconnect(true);
            return "Success";

        }
    }

    public class MailContent
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
