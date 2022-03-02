using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JDC.BusinessLogic.Utilities.AzureStorage
{
    public interface IAzureStorage
    {
        bool IsImage(IFormFile file);

        Task<string> UploadFileToStorage(Stream fileStream, string filePath);
    }
}
