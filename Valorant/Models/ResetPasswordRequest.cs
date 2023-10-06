﻿using System.ComponentModel.DataAnnotations;

namespace Valorant.Models
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please Enter 6 character!")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password", ErrorMessage = "Password doesnt match!")]
        public string ConfrimPassword { get; set; } = string.Empty;
    }
}
