using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Models;
using MoneyKeeper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "2TLKiqhrB6TP7DKqS0MjDBYoAyCGFfoIePKa7E7h",
            BasePath = "https://money-keeper-e4af2-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        [HttpPost("SendOTP")]
        public string ReceiveOTP(string email)
        {
            client = new FireSharp.FirebaseClient(config);
            //check user is existing in db?
            FirebaseResponse getUser = client.Get("Users");
            Users obj = getUser.ResultAs<Users>();
            var json = getUser.Body;
            Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);
            if (mailList != null)
            {
                foreach (KeyValuePair<string, Users> entry in mailList)
                {
                    if (entry.Value.email.ToString() == email)
                    {
                        SendOTP(email);
                        return JsonConvert.SerializeObject("OTP: " + randomCode);
                    }
                }
                return JsonConvert.SerializeObject("OTP: " + randomCode);
            }
            else
            {
                return JsonConvert.SerializeObject("Không tìm thấy tài khoản!");

            }
        }

        [HttpPost("ResetPassword")]
        public string ResetPassword(string email, string pass, string repass, string otp)
        {
            client = new FireSharp.FirebaseClient(config);

            if (otp == randomCode)
            {
                if (pass != repass)
                {
                    return JsonConvert.SerializeObject("Mật khẩu nhập lại không đúng!");
                }
                else
                {
                    FirebaseResponse getUser = client.Get("Users");
                    Users obj = getUser.ResultAs<Users>();
                    var json = getUser.Body;
                    Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);
                    if (mailList != null)
                    {
                        foreach (KeyValuePair<string, Users> entry in mailList)
                        {
                            if (entry.Value.email.ToString() == email)
                            {
                                string id = entry.Key;
                                obj.email = email;
                                obj.password = EncodePassword.MD5Hash(pass);
                                FirebaseResponse updateResponse = client.Update("Users/" + id, obj);
                                break;
                            }
                        }
                    }
                    return JsonConvert.SerializeObject("Đổi pass được ròi đó nhe");
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Nhập sai OTP rồi");
            }
        }
        static string randomCode;
        private void SendOTP(string email)
        {
            Random rand = new Random();
            randomCode = (rand.Next(100000, 999999)).ToString();
            var sendMailService = new SendMailService();
            var mailContent = new MailContent();

            mailContent.To = email;
            mailContent.Subject = "MONEY KEEPER VERIFY CODE";
            mailContent.Body = "Your OTP for Money Keeper: " + randomCode;

            var result = sendMailService.SendMail(mailContent);
        }
    }
}
