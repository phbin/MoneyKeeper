using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Models;
using MoneyKeeper.Services;
using MoneyKeeper.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IFirebaseClient client = DbService.ins.client;

        [HttpPost("signin")]
        public string Login(Users value)
        {
            FirebaseResponse getUser = client.Get("Users");
            var json = getUser.Body;
            Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);

            //check user is existing in db?
            if (mailList != null)
            {
                var existedUser = mailList.Where(item => item.Value.email == value.email).FirstOrDefault();
                if (existedUser.Value != null)
                {
                    var decodePass = EncodePassword.MD5Hash(value.password);
                    if (existedUser.Value.password == decodePass)
                        return JsonConvert.SerializeObject("Đăng nhập được rồi nè!");
                    else
                        return JsonConvert.SerializeObject("Sai tài khoản hoặc mật khẩu nha!");
                }
                else
                    return JsonConvert.SerializeObject("Tài khoản không tồn tại!");
            }

            return JsonConvert.SerializeObject("Tài khoản không tồn tại!");
            //return JsonConvert.SerializeObject("Đăng nhập được rồi nè!");
        }

        [HttpPost("signup")]
        public string Signup(Users value)
        {
            FirebaseResponse getUser = client.Get("Users");
            var json = getUser.Body;
            Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);

            //check user is existing in db?
            KeyValuePair<string, Users>? existedUser = null;
            if (mailList != null)
            {
                existedUser = mailList.Where(item => item.Value.email == value.email).FirstOrDefault();
            }

            if (existedUser != null)
            {
                return JsonConvert.SerializeObject("Người dùng đã tồn tại!");
                //ModelState.AddModelError(string.Empty, "Người dùng đã tồn tại!");
            }
            else
            {
                try
                {
                    value.password = EncodePassword.MD5Hash(value.password);
                    var code = SendOTP(value.email);

                    //save code to waiting list
                    WaitingUser wu = new WaitingUser { data = value, code = code, };
                    SignupQueue[value.email] = wu;

                    return JsonConvert.SerializeObject(value);
                }
                catch (Exception e)
                {
                    return JsonConvert.SerializeObject(e.Message);
                }
            }
        }

        [HttpPost("verify-signup")]
        public string VerifySignup(string email, string otp)
        {
            if (SignupQueue[email].code == otp)
            {
                PushResponse setResponse = client.Push("Users", SignupQueue[email].data);
                SignupQueue.Remove(email);
                return JsonConvert.SerializeObject("Đăng ký được gòi đó");
            }
            return JsonConvert.SerializeObject("Sai mã rồi!");
        }

        [HttpPost("forgot-pass")]
        public string ForgotPassword(string email)
        {
            FirebaseResponse getUser = client.Get("Users");
            var json = getUser.Body;
            Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);

            //check user is existing in db?
            if (mailList != null)
            {
                var existedUser = mailList.Where(item => item.Value.email == email).FirstOrDefault();

                if (existedUser.Value != null)
                {
                    //add code to reset pass waiting list
                    var code = SendOTP(email);
                    ResetPasswordQueue[email] = code;
                    return JsonConvert.SerializeObject(email);
                }
                else
                    return JsonConvert.SerializeObject("Tài khoản không tồn tại!");
            }
            else
            {
                return JsonConvert.SerializeObject("Không tìm thấy tài khoản!");
            }
        }

        [HttpPost("reset-pass")]
        public string ResetPassword(string email, string pass, string otp)
        {
            if (ResetPasswordQueue[email] == otp)
            {
                FirebaseResponse getUser = client.Get("Users");
                Users obj = getUser.ResultAs<Users>();
                var json = getUser.Body;
                Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);
                if (mailList != null)
                {
                    var user = mailList.Where(item => item.Value.email == email).FirstOrDefault();

                    if (user.Value != null)
                    {
                        obj.email = email;
                        obj.password = EncodePassword.MD5Hash(pass);
                        FirebaseResponse updateResponse = client.Update("Users/" + user.Key, obj);
                        ResetPasswordQueue.Remove(email);
                    }
                }
                return JsonConvert.SerializeObject("Đổi pass được ròi đó nhe");
            }
            else
            {
                return JsonConvert.SerializeObject("Nhập sai OTP rồi");
            }
        }


        Dictionary<string, WaitingUser> SignupQueue { get; set; }
        Dictionary<string, string> ResetPasswordQueue { get; set; }

        private string SendOTP(string email)
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

        class WaitingUser
        {
            public Users data { get; set; }
            public string code { get; set; }
        }
    }
}
