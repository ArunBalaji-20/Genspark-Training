using Microsoft.AspNetCore.Http.Metadata;

namespace BugTrackingAPI.Interfaces
{
    public interface IScreenshotStorageService
    {
        Task<string> UploadScreenshot(IFormFile file,string filename);
    }
}