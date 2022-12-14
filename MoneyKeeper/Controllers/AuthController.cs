using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Data;
using MoneyKeeper.Data.Users;
using MoneyKeeper.Models;
using MoneyKeeper.Services;
using MoneyKeeper.Services.Auth;
using MoneyKeeper.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
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

            if (result == (null, "not found"))
            {
                return NotFound(new ApiResponse<string>(string.Empty, "Account does not exist"));
            }
            else if(result == (null, "wrong"))
            {
                return Unauthorized(new ApiResponse<string>(string.Empty, "Incorrect password"));
            }
            var user = _mapper.Map<Users>(result.Item1);

            return Ok(new ApiResponse<Users>(user, "Login successfully."));
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUp signUpUser)
        {
            var result = await _authService.SignUp(signUpUser);
            if (result==(null,null))
            {
                return NotFound(new ApiResponse<string>(string.Empty,"This email has already existed"));
            }
            return Ok(new ApiResponse<string>(string.Empty, "An email with verification code was sent"));
        }
    
        [HttpPost("verify-account")]
        public async Task<IActionResult> VerifyAccount([FromBody] OneTimePassword code)
        {
            var result = await _authService.VerifyAccountSignUp(code);

            if (result==(null,null))
            {
                return NotFound(new ApiResponse<string>(string.Empty,"Invalid OTP code"));
            }
            var user = _mapper.Map<Users>(result.Item1);
            return Ok(new ApiResponse<Users>(user, "Verify account successfully!"));
        }
      
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var result=await _authService.ForgotPassword(email);
            if (result ==  null)
            {
                return NotFound(new ApiResponse<string>(string.Empty, "User not found!"));
            }
            return Ok(new ApiResponse<string>(email, "An email with forgot password verification code was sent"));

        }

        [HttpPost("verify-reset-password")]
        public async Task<IActionResult> VerifyResetPassword([FromBody] OneTimePassword code)
        {

            var result = await _authService.VerifyResetPassword(code);
            if (result == null)
            {
                return NotFound(new ApiResponse<string>(string.Empty, "Invalid OTP code"));
            }
            return Ok(new ApiResponse<string>(string.Empty, "Verify reset password successfully!"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword newPassword)
        {
            var result = await _authService.ResetPassword(newPassword);
            if(result==(null,null))
            {
                return NotFound(new ApiResponse<string>(string.Empty, "Confirm password does not match"));
            }
            var user = _mapper.Map<Users>(result.Item1);
            return Ok(new ApiResponse<Users>(user,"Password changed!"));
        }
    }
}
