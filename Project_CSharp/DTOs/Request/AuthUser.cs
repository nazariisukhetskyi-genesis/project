using System.ComponentModel.DataAnnotations;

namespace Project_CSharp.DTOs.Request
{
    public class AuthUser
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid!")]
        public string Email { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Minimal length of password should be 6!")]
        public string Password { get; set; }
    }
}
