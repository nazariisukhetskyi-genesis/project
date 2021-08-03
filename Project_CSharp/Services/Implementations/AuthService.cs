using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project_CSharp.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Project_CSharp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public static Tuple<string, string> GetHashedPasswordAndSalt(string password)
        {
            byte[] saltBytes = new byte[16];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(saltBytes);

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10_000,
                numBytesRequested: 32
            ));

            string salt = Convert.ToBase64String(saltBytes);

            return new(hashedPassword, salt);
        }

        public JwtSecurityToken GetToken()
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: "http://localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            return token;
        }

        public string GetUnhashedPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            var unhashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10_000,
                numBytesRequested: 32));

            return unhashedPassword;
        }
    }
}