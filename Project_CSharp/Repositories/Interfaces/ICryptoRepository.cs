using System.Threading.Tasks;

namespace Project_CSharp.Repositories.Interfaces
{
    public interface ICryptoRepository
    {
        Task<string> GetFullRateAsync_JSON();
        Task<string> GetBtcRateAsync();
    }
}
