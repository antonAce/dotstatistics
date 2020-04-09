using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Regression.API.Interfaces;

namespace Regression.API.Helpers
{
    public class FileParser : IFileParser
    {
        public async Task<byte[]> GetBytesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty!");
            else if (Path.GetExtension(file.FileName) != ".csv")
                throw new ArgumentException("Wrong file format!");
            else
            {
                await using MemoryStream ms = new MemoryStream();
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task<string> GetStringAsync(IFormFile file)
        {
            var bytes = await GetBytesAsync(file);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}