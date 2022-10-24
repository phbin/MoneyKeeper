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
using static System.Net.WebRequestMethods;
using Users = MoneyKeeper.Models.Users;

namespace MoneyKeeper.Services.Auth
{
    public class AuthService : IAuthService
    {
        public DataContext _context { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private static IDictionary<string, SaveCode> listWaitingCode = new Dictionary<string, SaveCode>();

        public AuthService(IConfiguration configuration, DataContext context, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<(Users,string)> SignUp(SignUp user)
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
                    Users newUser = new Users
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

        public async Task<string> SignIn(SignIn user)
        {
            var rs = EncodePassword.MD5Hash(user.password);

            var result = await _context.Users.FirstOrDefaultAsync(x => x.email.ToLower().Equals(user.email.ToLower()));
            //user not found
            if (result == null)
            {
                return null;
            }
            //wrong password
            else if (result.password != rs)
            {
                return null;
            }
            return ("sign-in success");
        }

        public async Task<(Users,string)> VerifyAccountSignUp(OneTimePassword code)
        {
            SaveCode newCode;
            if (!listWaitingCode.TryGetValue(code.email, out newCode!) || code.otp != newCode.otp)
            {
                return (null, null);
            }
            Users newUser = new Users
            {
                email = newCode.user.email,
                password = EncodePassword.MD5Hash(newCode.user.password),
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            listWaitingCode.Remove(code.email);
            return (newUser, "Sign up success");
        }
    }
}
