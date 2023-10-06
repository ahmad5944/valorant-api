using System.ComponentModel.DataAnnotations;

namespace Valorant.Models
{
    public class UserRegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please Enter 6 character!")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password", ErrorMessage = "Password doesnt match!")]
        public string ConfrimPassword {  get; set; } = string.Empty;
    }
}
