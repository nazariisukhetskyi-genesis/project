using Project_CSharp.DTOs.Request;
using Project_CSharp.Services;
using System.Threading.Tasks;

namespace Project_CSharp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Answer> CreateAsync(AuthUser authUser);
        Task<AuthHelper> LoginAsync(AuthUser authUser);
        Task<AuthHelper> UpdateAccessTokenAsync(string refreshToken);
    }
}
