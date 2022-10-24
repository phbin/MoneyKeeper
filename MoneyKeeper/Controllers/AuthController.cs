using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Data;
using MoneyKeeper.Data.Users;
using MoneyKeeper.Models;
using MoneyKeeper.Services;
using MoneyKeeper.Services.Auth;
using MoneyKeeper.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Users = MoneyKeeper.Data.Users.Users;

namespace MoneyKeeper.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public IMapper _mapper { get; set; }

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn signInUser)
        {
            var result = await _authService.SignIn(signInUser);
            if (string.IsNullOrEmpty(result))
            {
                return NotFound();
            }
            return Ok(new ApiResponse<SignIn>(signInUser, "Login successfully."));
        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> Register([FromBody] SignUp signUpUser)
        {
            var result = await _authService.SignUp(signUpUser);
            if (result==(null,null))
            {
                return NotFound();
            }
            return Ok(new ApiResponse<SignUp>(signUpUser, "An email with verification code was sent"));
        }
    
        [HttpPost("verify-account")]
        public async Task<IActionResult> VerifyEmailToken([FromBody] OneTimePassword code)
        {
            var result = await _authService.VerifyAccountSignUp(code);
            if(result==(null,null))
            {
                return NotFound();
            }
            return Ok(new ApiResponse<OneTimePassword>(code, "Verify account successfully!"));
        }
      
        //[HttpPost("forgot-pass")]
        //public string ForgotPassword(string email)
        //{
        //    FirebaseResponse getUser = client.Get("Users");
        //    var json = getUser.Body;
        //    Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);

        //    //check user is existing in db?
        //    if (mailList != null)
        //    {
        //        var existedUser = mailList.Where(item => item.Value.email == email).FirstOrDefault();

        //        if (existedUser.Value != null)
        //        {
        //            //add code to reset pass waiting list
        //            var code = SendOTP(email);
        //            ResetPasswordQueue[email] = code;
        //            return JsonConvert.SerializeObject(email);
        //        }
        //        else
        //            return JsonConvert.SerializeObject("Tài khoản không tồn tại!");
        //    }
        //    else
        //    {
        //        return JsonConvert.SerializeObject("Không tìm thấy tài khoản!");
        //    }
        //}

        // [HttpPost("reset-pass")]
        //public string ResetPassword(string email, string pass, string otp)
        //{
        //    if (ResetPasswordQueue[email] == otp)
        //    {
        //        FirebaseResponse getUser = client.Get("Users");
        //        Users obj = getUser.ResultAs<Users>();
        //        var json = getUser.Body;
        //        Dictionary<string, Users> mailList = JsonConvert.DeserializeObject<Dictionary<string, Users>>(json);
        //        if (mailList != null)
        //        {
        //            var user = mailList.Where(item => item.Value.email == email).FirstOrDefault();

        //            if (user.Value != null)
        //            {
        //                obj.email = email;
        //                obj.password = EncodePassword.MD5Hash(pass);
        //                FirebaseResponse updateResponse = client.Update("Users/" + user.Key, obj);
        //                ResetPasswordQueue.Remove(email);
        //            }
        //        }
        //        return JsonConvert.SerializeObject("Đổi pass được ròi đó nhe");
        //    }
        //    else
        //    {
        //        return JsonConvert.SerializeObject("Nhập sai OTP rồi");
        //    }
        //}
    }
}
