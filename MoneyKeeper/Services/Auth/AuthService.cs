﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyKeeper.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MoneyKeeper.Dtos.Auth;
using MoneyKeeper.Dtos.User;
using MoneyKeeper.Data;
using MoneyKeeper.Dtos;

namespace MoneyKeeper.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly Mail.IMailService _mailService;
        private static IDictionary<string, Token> ListTokenAccount = new Dictionary<string, Token>();
        private static IDictionary<string, Token> ListForgotPasswordAccount = new Dictionary<string, Token>();
        private static IDictionary<string, string> ListResetPasswordAccount = new Dictionary<string, string>();
        public DataContext _context { get; set; }

        public AuthService(IConfiguration configuration, DataContext context, IMapper mapper, Services.Mail.IMailService mailService)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _mailService = mailService;
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                }),
                Expires = DateTime.Now.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ValidateToken(string token)
        {
            if (token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                return userId;
            }
            catch
            {
                return null;
            }
        }

        public async Task<(User, string)> Login(LoginUserDto userDto)
        {
            var users = await _context.Users.ToListAsync();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
            if (user == null)
            {
                throw new ApiException("User not found!", 400);
            }
            else if (user.Password != userDto.Password)
            {
                throw new ApiException("Wrong email or password!", 400);
            }
            return (user, CreateToken(user));
        }

        public async Task Register(RegisterUserDto userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(userDTO.Email.ToLower()));
            if (user != null)
            {
                throw new ApiException("Email have already existed!", 400);
            }
            Token token;
            if (ListTokenAccount.TryGetValue(userDTO.Email, out token!))
            {
                if (token.ExpiredAt > DateTime.Now)
                {
                    throw new ApiException("Please try again in 2 minutes", 400);
                }
                ListTokenAccount.Remove(userDTO.Email);
            }
            var tokenCode = await _mailService.SendRegisterMail(userDTO.Email);
            var code = new Token { Code = tokenCode, ExpiredAt = DateTime.Now.AddMinutes(2), User = userDTO };
            ListTokenAccount.Add(userDTO.Email, code);
        }

        public async Task<bool> FindUserByEmai(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower()) == null ? false : true;
        }

        public async Task<(User, string)> VerifyEmailToken(TokenDTO tokenDTO)
        {
            Token token;
            if (!ListTokenAccount.TryGetValue(tokenDTO.Email, out token!) || tokenDTO.Code != token.Code || token.ExpiredAt < DateTime.Now)
            {
                throw new ApiException("Code is wrong or expired!", 400);
            }
            User user = new User
            {
                Email = token.User.Email,
                Password = token.User.Password,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            ListTokenAccount.Remove(tokenDTO.Email);
            return (user, CreateToken(user));
        }

        public async Task ForgotPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                throw new ApiException("User not found.", 400);
            }
            Token token;
            if (ListForgotPasswordAccount.TryGetValue(email, out token!))
            {
                if (token.ExpiredAt > DateTime.Now)
                {
                    throw new ApiException("Please try again in 2 minutes", 400);
                }
                ListForgotPasswordAccount.Remove(email);
            }
            var rePasswordCode = await _mailService.SendResetPasswordMail(email);
            ListForgotPasswordAccount.Add(email, new Token { Code = rePasswordCode, ExpiredAt = DateTime.Now.AddMinutes(2) });
        }

        public string VerifyResetPassword(string email, string value)
        {
            Token token;
            if (!ListForgotPasswordAccount.TryGetValue(email, out token!) || value != token.Code)
            {
                throw new ApiException("Code is wrong or expired!", 400);
            }
            ListForgotPasswordAccount.Remove(email);
            string code = CreateRandomToken();
            ListResetPasswordAccount.Add(code, email);
            return code;
        }

        public async Task<(User, string)> ResetPassword(TokenResetPasswordDto user)
        {
            string email;
            if (!ListResetPasswordAccount.TryGetValue(user.Token, out email!))
            {
                throw new ApiException("Invalid Token", 400);
            }
            var _user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (_user == null)
            {
                throw new ApiException("User not found.", 400);
            }
            ListResetPasswordAccount.Remove(user.Token);
            _user.Password = user.Password;
            await _context.SaveChangesAsync();
            return (_user, CreateToken(_user));
        }

        public async Task ChangePassword(ChangePassword chUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(chUser.Email.ToLower()));
            if (user == null)
            {
                throw new ApiException("User not found.", 400);
            }
            else if (user.Password != chUser.Password)
            {
                throw new ApiException("Wrong current password!", 400);
            }
            user.Password = chUser.Password;
            await _context.SaveChangesAsync();
        }

        private string CreateRandomToken()
        {
            return new Random().Next(1000000, 9999999).ToString();
        }
    }
}
