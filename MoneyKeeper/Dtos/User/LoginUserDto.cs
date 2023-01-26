﻿using System.ComponentModel.DataAnnotations;

namespace MoneyKeeper.Dtos
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required, MinLength(6, ErrorMessage = "Password is required and must be at least 6 character")]
        public string Password { get; set; }
        public LoginUserDto()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
