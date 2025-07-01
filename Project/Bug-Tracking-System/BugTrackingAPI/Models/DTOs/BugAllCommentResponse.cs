namespace BugTrackingAPI.Models.DTO
{
    public class BugAllCommentsResponse
    {
        public long CommentId{ get; set; }
        public string Comment { get; set; } = string.Empty;

        public DateTime CommentedOn { get; set; }

        public long BugId { get; set; }

        public long CommenterId { get; set; }

        public string CommenterName { get; set; } = string.Empty;


    }
}