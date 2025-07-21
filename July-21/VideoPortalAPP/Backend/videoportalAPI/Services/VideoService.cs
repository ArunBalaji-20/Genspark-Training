
using Azure.Storage.Blobs;
using videoportalAPI.Context;
using videoportalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace videoportalAPI.Services
{
    public class VideoService
    {

        private readonly BlobContainerClient _containerClinet;
        private readonly VideosContext _videosContext;

        public VideoService(IConfiguration configuration, VideosContext videosContext)
        {
            var sasUrl = configuration["AzureBlob:ContainerSasUrl"];
            _containerClinet = new BlobContainerClient(new Uri(sasUrl));
            _videosContext = videosContext;
        }

        public async Task UploadFile(Stream fileStream, string fileName, string Description)
        {
            var blobClient = _containerClinet.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);

            var newFile = new TrainingVideos
            {
                Title = fileName,
                Description = Description,
                BlobUrl = blobClient.Uri.ToString(),
                UploadDate = DateTime.UtcNow
            };

            _videosContext.TrainingVideos.Add(newFile);
            await _videosContext.SaveChangesAsync();


        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            var blobClient = _containerClinet?.GetBlobClient(fileName);
            if (await blobClient.ExistsAsync())
            {
                var downloadInfor = await blobClient.DownloadStreamingAsync();
                return downloadInfor.Value.Content;
            }
            return null;
        }
        public async Task<IEnumerable<TrainingVideos>> GetList()
        {
            var result = await _videosContext.TrainingVideos.ToListAsync();
            return result;
        }

    }
}



