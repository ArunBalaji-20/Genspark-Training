using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Repositories;

namespace BugTrackingAPI.Services
{
    public class BugCommentService : IBugCommentService
    {
        private readonly IRepository<long, BugComment> _bugCommentRepo;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRepository<long, Bugs> _bugRepo;

        public BugCommentService(IRepository<long, BugComment> BugCommentRepo,
                                IEmployeeRepository employeeRepository,
                                IRepository<long, Bugs> BugRepo)
        {
            _bugCommentRepo = BugCommentRepo;
            _employeeRepository = employeeRepository;
            _bugRepo = BugRepo;
        }
        public async Task<BugCommentResponse> AddComment(BugCommentRequest bugCommentReq, string email)
        {
            long commenterId = await GetBugSubmitterId(email);
            var BugData = await _bugRepo.Get(bugCommentReq.BugId);
            if (BugData == null)
            {
                throw new Exception("Invalid BugId");
            }


            var BugComment = new BugComment
            {
                Comment = bugCommentReq.Comment,
                CommentedOn = DateTime.UtcNow,
                BugId = bugCommentReq.BugId,
                CommenterId = commenterId
            };

            await _bugCommentRepo.Add(BugComment);

            var result = new BugCommentResponse
            {
                Comment = BugComment.Comment,
                CommentedOn = BugComment.CommentedOn,
                BugId = BugComment.BugId,
                CommenterId=BugComment.CommenterId
            };
            return result;

        }

        public async Task<IEnumerable<string>> GetAllCommentsForBug(long bugId)
        {
             var allComments = await _bugCommentRepo.GetAll();
             var comments = allComments
                .Where(e => e.BugId == bugId)
                .Select(f => f.Comment )
                .ToList();

            return comments;
        }

        public async Task<string> DeleteComments(long commentId,string email,string role)
        {
            long commenterId = await GetBugSubmitterId(email);
            var comment = await _bugCommentRepo.Get(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }
            if (role == "Admin")
            {
                 var AdminDeletes=await _bugCommentRepo.Delete(commentId);
                return "Comment Deleted Successfully";
            }
            if (comment.CommenterId != commenterId)
            {
                throw new UnauthorizedAccessException("Not authorized");
            }
            var result=await _bugCommentRepo.Delete(commentId);
            return "Comment Deleted Successfully";
        }


        public async Task<IEnumerable<BugAllCommentsResponse>> GetAllCommentsForBugV2(long bugId)
        {
            var allComments = await _bugCommentRepo.GetAll();
            // var comments = allComments
            // .Where(e => e.BugId == bugId)
            // .Select(async f => new BugAllCommentsResponse
            // {
            //     Comment = f.Comment,
            //     BugId = f.BugId,
            //     CommentedOn = f.CommentedOn,
            //     CommenterId = f.CommenterId,
            //     CommenterName= await GetCommenterName(f.CommenterId)
            // })
            // .ToList();
            // System.Console.WriteLine(comments);

            // var commentTasks = allComments
            //     .Where(e => e.BugId == bugId)
            //     .Select(async f => new BugAllCommentsResponse
            //     {
            //         Comment = f.Comment,
            //         BugId = f.BugId,
            //         CommentedOn = f.CommentedOn,
            //         CommenterId = f.CommenterId,
            //         CommenterName = await GetCommenterName(f.CommenterId)
            //     });

            // var comments = await Task.WhenAll(commentTasks);
            
            var filtered = allComments.Where(e => e.BugId == bugId);

            var comments = new List<BugAllCommentsResponse>();

            foreach (var f in filtered)
            {
                var commenterName = await GetCommenterName(f.CommenterId); // Safe: one at a time

                comments.Add(new BugAllCommentsResponse
                {
                    Comment = f.Comment,
                    BugId = f.BugId,
                    CommentedOn = f.CommentedOn,
                    CommenterId = f.CommenterId,
                    CommenterName = commenterName
                });
            }


            return comments;
        }

        private async Task<string> GetCommenterName(long id)
        {
            var employee = await _employeeRepository.Get(id);
            var name = employee.Name;
            return name;
        }
        private async Task<long> GetBugSubmitterId(string email)
        {
            var result = await _employeeRepository.GetEmployeeByEmail(email);
            return result.EmployeeId;
        }
    }
}