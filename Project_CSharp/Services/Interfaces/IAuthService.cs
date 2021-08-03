using System.IdentityModel.Tokens.Jwt;

namespace Project_CSharp.Services.Interfaces
{
    public interface IAuthService
    {
        JwtSecurityToken GetToken();
        string GetUnhashedPassword(string password, string salt);
    }
}
