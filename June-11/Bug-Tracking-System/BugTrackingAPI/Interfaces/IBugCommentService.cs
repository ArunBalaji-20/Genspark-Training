
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Interfaces
{
    public interface IBugCommentService
    {
        public Task<BugCommentResponse> AddComment(BugCommentRequest bugCommentReq, string email);


        public Task<IEnumerable<string>> GetAllCommentsForBug(long bugId);

        public Task<string> DeleteComments(long commentId,string email);
    }
}