using Project_CSharp.Models;
using System.Threading.Tasks;

namespace Project_CSharp.Services.Interfaces
{
    public interface IFileService
    {
        Task WriteUserToFileAsync(User user);
        Task OverwriteFileAsync();
    }
}
