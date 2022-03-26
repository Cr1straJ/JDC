using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using JDC.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;

namespace JDC.BusinessLogic.Utilities.ImageStorage
{
    /// <summary>
    /// Provides methods of working with the azure data storage.
    /// </summary>
    public class AzureStorage : IImageStorage
    {
        private readonly AzureStorageConfig azureStorageConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorage"/> class.
        /// </summary>
        /// <param name="azureStorageConfig">Settings for the azure storage.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="azureStorageConfig"/> is null.
        /// </exception>
        public AzureStorage(AzureStorageConfig azureStorageConfig)
        {
            this.azureStorageConfig = azureStorageConfig
                ?? throw new ArgumentNullException(nameof(azureStorageConfig));
        }

        /// <summary>
        /// Verifies if the file is an image.
        /// </summary>
        /// <param name="file">Source file.</param>
        /// <returns>true if file is an image, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="file"/> is null.
        /// </exception>
        public bool IsImage(IFormFile file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.ContentType.Contains("image", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// The <see cref="UploadFileToStorage"/> operation loads
        /// a file from the stream to the blob storage.
        /// </summary>
        /// <param name="fileStream">A Stream containing the content to upload..</param>
        /// <param name="fileName">Source file name.</param>
        /// <returns>A file path containing the content to upload.</returns>
        public async Task<string> UploadFileToStorage(Stream fileStream, string fileName)
        {
            var filePath = GetImageUrl(fileName);
            var blobUri = new Uri(filePath);

            var storageCredentials = new StorageSharedKeyCredential(azureStorageConfig.AccountName, azureStorageConfig.AccountKey);

            var blobClient = new BlobClient(blobUri, storageCredentials);
            await blobClient.UploadAsync(fileStream);
            await Task.FromResult(true);

            return filePath;
        }

        private string GetImageUrl(string fileName)
            => $"https://{azureStorageConfig.AccountName}.blob.core.windows.net/{azureStorageConfig.ImageContainer}/{fileName}";
    }
}
