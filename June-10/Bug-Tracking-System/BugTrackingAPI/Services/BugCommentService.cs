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
        private async Task<long> GetBugSubmitterId(string email)
        {
            var result = await _employeeRepository.GetEmployeeByEmail(email);
            return result.EmployeeId;
        }
    }
}