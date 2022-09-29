using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoneyKeeper.Models;
using MoneyKeeper.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyKeeper.Controllers
{
    [Route("auth")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "2TLKiqhrB6TP7DKqS0MjDBYoAyCGFfoIePKa7E7h",
            BasePath= "https://money-keeper-e4af2-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        //// GET: api/<SignUpController>
        //[HttpGet]
        //public int Get()
        //{
            
        //}

        //// GET api/<SignUpController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
      
        [HttpPost("OTPGeneration")]
        public string GenerateOTP(Users value)
        {
            int count = 0;
            client = new FireSharp.FirebaseClient(config);
            //check user is existing in db?
            FirebaseResponse getUser =client.Get("Users");
            Users obj = getUser.ResultAs<Users>();
            var json = getUser.Body;
            Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);
            if (mailList != null)
            {
                foreach (KeyValuePair<string, Users> entry in mailList)
                {
                    if (entry.Value.email.ToString() == value.email) count++;
                }
            }          
            //add account
            if (count==0){
                try
                {
                    value.password = EncodePassword.MD5Hash(value.password);
                    SendOTP(value.email);
                    PushResponse setResponse = client.Push("Users", value);
                    return JsonConvert.SerializeObject("Email: "+value.email+" OTP: "+randomCode);
                    //ModelState.AddModelError(string.Empty, "Đăng ký thành công.");
                }
                catch (Exception e)                                                                                                                                             
                {
                    return JsonConvert.SerializeObject(e.Message);
                    //ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            else
            {
                return JsonConvert.SerializeObject("Người dùng đã tồn tại!");
                //ModelState.AddModelError(string.Empty, "Người dùng đã tồn tại!");
            }
        }

        /// <summary>
        /// Validate OTP
        /// </summary>
        /// <param name="otp"></param>
        /// <returns></returns>
        [HttpPost("OTPValidation")]
        public string ValidateOTP(string email, string otp)
        {
            //check user is existing in db?
            if (otp == randomCode)
            {
                return JsonConvert.SerializeObject("Đăng ký được gòi đó");
            }
            else
            {
                client = new FireSharp.FirebaseClient(config);

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
                            FirebaseResponse delResponse = client.Delete("Users/" + id);
                            break;
                        }
                    }
                }
                return JsonConvert.SerializeObject("Sai mã rồi!");
            }
        }
        static string randomCode;
        private void SendOTP(string email)
        {
            Random rand = new Random();
            randomCode = (rand.Next(100000,999999)).ToString();
            var sendMailService = new SendMailService();
            var mailContent = new MailContent();

            mailContent.To = email;
            mailContent.Subject = "MONEY KEEPER VERIFY CODE";
            mailContent.Body = "Your OTP for Money Keeper: "+randomCode;

            var result =  sendMailService.SendMail(mailContent);
            //return randomCode;
        }


        //// PUT api/<SignUpController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SignUpController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
