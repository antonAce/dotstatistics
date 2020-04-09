using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Regression.API.Interfaces
{
    public interface IFileParser
    {
        Task<byte[]> GetBytesAsync(IFormFile file);
        Task<string> GetStringAsync(IFormFile file);
    }
}