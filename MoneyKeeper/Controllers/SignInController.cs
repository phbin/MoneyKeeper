using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Models;
using MoneyKeeper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "2TLKiqhrB6TP7DKqS0MjDBYoAyCGFfoIePKa7E7h",
            BasePath = "https://money-keeper-e4af2-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        // POST api/<SignInController>
        [HttpPost]
        public string LogIn([FromBody] Users value)
        {
            int count = 0;
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
                    if (entry.Value.email.ToString() == value.email) count++;
                }
            }
            if (count != 0)
            {
                foreach (KeyValuePair<string, Users> entry in mailList)
                {
                    if (entry.Value.email.ToString() == value.email)
                    {
                        var decodePass = EncodePassword.MD5Hash(value.password);
                        if (entry.Value.password.ToString() == decodePass) return JsonConvert.SerializeObject("Đăng nhập được rồi nè!");
                      
                        else return JsonConvert.SerializeObject("Sai mật khẩu nha!");
                    }
                }
                return JsonConvert.SerializeObject("Đăng nhập được rồi nè!");
            }
            else
            {
                return JsonConvert.SerializeObject("Tài khoản không tồn tại!");
                //return JsonConvert.SerializeObject("Đăng nhập được rồi nè!");
            }
        }
    }
}
