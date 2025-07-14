
using Azure.Storage.Blobs;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Models.DTOs;

namespace BugTrackingAPI.Services
{
    public class AzureScreenshotStorageService : IScreenshotStorageService
    {
            private readonly BlobContainerClient _containerClient;

        public AzureScreenshotStorageService(IConfiguration config)
        {
            var connectionString = config["Storage:ConnectionString"];
            var containerName = config["Storage:ScreenshotsContainer"];
            _containerClient = new BlobServiceClient(connectionString).GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task<string> UploadScreenshot(IFormFile file,string filename)
        {
            var blobClient = _containerClient.GetBlobClient(filename);
            await blobClient.UploadAsync(file.OpenReadStream(), overwrite: true);
            return blobClient.Uri.ToString(); // Can be private/SAS URL in real use
        }

        // public Task<string> UploadScreenshot(IFormFile file)
        // {

        // }
    }
}