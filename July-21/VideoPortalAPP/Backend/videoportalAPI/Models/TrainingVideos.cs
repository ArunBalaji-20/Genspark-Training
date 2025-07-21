namespace videoportalAPI.Models
{
    public class TrainingVideos
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; }

        public string BlobUrl { get; set; } = string.Empty;
    }
}

// CREATE TABLE TrainingVideos (
//   Id INT IDENTITY PRIMARY KEY,
//   Title NVARCHAR(200),
//   Description NVARCHAR(500),
//   UploadDate DATETIME,
//   BlobUrl NVARCHAR(500)
// );