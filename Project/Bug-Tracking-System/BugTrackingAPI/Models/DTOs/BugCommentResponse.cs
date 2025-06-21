namespace BugTrackingAPI.Models.DTO
{
    public class BugCommentResponse
    {

        public string Comment { get; set; } = string.Empty;

        public DateTime CommentedOn { get; set; }

        // Foreign Keys
        public long BugId { get; set; }

        public long CommenterId { get; set; }


    }
}