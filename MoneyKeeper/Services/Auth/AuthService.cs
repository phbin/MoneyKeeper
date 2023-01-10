using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyKeeper.Data.Users;
using MoneyKeeper.Models;
using MoneyKeeper.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Services.Auth
{
    public class AuthService : IAuthService
    {
        public DataContext _context { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private static IDictionary<string, SaveCode> listWaitingCode = new Dictionary<string, SaveCode>();
        private static IDictionary<string, string> listResetPassword = new Dictionary<string, string>();

        public AuthService(IConfiguration configuration, DataContext context, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<(User,string)> SignUp(SignUp user)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.email.ToLower().Equals(user.email.ToLower()));
            if (result != null)
            {
                //account has been existed
                return (null,null);
            }
            else
            {
                try
                {
                    User newUser = new User
                    {
                        email = user.email,
                        password = user.password,
                    };

                    var sentCode = SendOTP(user.email);
                    var saveCode = new SaveCode { otp = sentCode, user = newUser };

                    listWaitingCode.Add(newUser.email, saveCode);

                    return (newUser,sentCode);
                }
                catch (Exception e)
                {
                    return (null,JsonConvert.SerializeObject(e.Message));
                }
            }
        }

        public string SendOTP(string email)
        {
            Random rand = new Random();
            var randomCode = (rand.Next(100000, 999999)).ToString();
            var sendMailService = new SendMailService();
            var mailContent = new MailContent();

            mailContent.To = email;
            mailContent.Subject = "MONEY KEEPER VERIFY CODE";
            mailContent.Body = "Your OTP for Money Keeper: " + randomCode;

            _ = sendMailService.SendMail(mailContent);

            return randomCode;
        }

        public async Task<(User,string)> SignIn(SignIn user)
        {
            var rs = EncodePassword.MD5Hash(user.password);

            var result = await _context.Users.FirstOrDefaultAsync(x => x.email.ToLower().Equals(user.email.ToLower()));
            //user not found
            if (result == null)
            {
                return (null,"not found");
            }
            //wrong password
            else if (result.password != rs)
            {
                return (null,"wrong");
            }
            return (result,"sign-in success");
        }

        public async Task<(User,string)> VerifyAccountSignUp(OneTimePassword code)
        {
            SaveCode newCode;
            if (!listWaitingCode.TryGetValue(code.email, out newCode!) || code.otp != newCode.otp)
            {
                return (null, null);
            }
            User newUser = new User
            {
                email = newCode.user.email,
                password = EncodePassword.MD5Hash(newCode.user.password),
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            listWaitingCode.Remove(code.email);
            return (newUser, "Sign up success");
        }

        public async Task<string> ForgotPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                //user not found
                return null;
            }
            else
            {
                try
                {
                    var sentCode = SendOTP(email);
                    var saveCode = new OneTimePassword { email = email, otp = sentCode };

                    listResetPassword.Add(email, sentCode);

                    return (sentCode);
                }
                catch (Exception e)
                {
                    return JsonConvert.SerializeObject(e.Message);
                }
            }
        }

        public async Task<(User,string)> ResetPassword(ResetPassword code)
        {
            if (code.newPassword != code.retypePassword)
            {
                //retype password was wrong
                return (null,null);
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.email.ToLower().Equals(code.email.ToLower()));

            user.password = EncodePassword.MD5Hash(code.newPassword);
            await _context.SaveChangesAsync();
            listResetPassword.Remove(code.email);
            return (user,"Password changed!");
        }

        public async Task<string> VerifyResetPassword(OneTimePassword code)
        {
            string newCode;
            if (!listResetPassword.TryGetValue(code.email, out newCode!) || code.otp != newCode)
            {
                //wrong code
                return null;
            }
            return "Next step!";
        }
    }
}
