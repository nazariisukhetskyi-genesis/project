using System;

namespace Project_CSharp.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }

        public override string ToString()
        {
            return $"{Email},{Password},{Salt},{RefreshToken},{RefreshTokenExpiration}";
        }
    }
}
