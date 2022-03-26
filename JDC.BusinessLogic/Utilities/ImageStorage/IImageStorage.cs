using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JDC.BusinessLogic.Utilities.ImageStorage
{
    public interface IImageStorage
    {
        bool IsImage(IFormFile file);

        Task<string> UploadFileToStorage(Stream fileStream, string filePath);
    }
}
