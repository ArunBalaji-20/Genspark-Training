using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Services;
using Moq;
using Xunit;

public class BugCommentServiceTests
{
    private readonly Mock<IRepository<long, BugComment>> _mockBugCommentRepo;
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
    private readonly Mock<IRepository<long, Bugs>> _mockBugRepo;
    private readonly BugCommentService _service;

    public BugCommentServiceTests()
    {
        _mockBugCommentRepo = new Mock<IRepository<long, BugComment>>();
        _mockEmployeeRepo = new Mock<IEmployeeRepository>();
        _mockBugRepo = new Mock<IRepository<long, Bugs>>();

        _service = new BugCommentService(
            _mockBugCommentRepo.Object,
            _mockEmployeeRepo.Object,
            _mockBugRepo.Object);
    }

    [Fact]
    public async Task AddComment_ValidRequest_ReturnsResponse()
    {
        // Arrange
        var request = new BugCommentRequest { BugId = 1, Comment = "Test Comment" };
        var email = "dev@example.com";
        var bug = new Bugs { BugId = 1 };
        var employee = new Employee { EmployeeId = 101, Email = email };

        _mockEmployeeRepo.Setup(r => r.GetEmployeeByEmail(email))
                         .ReturnsAsync(employee);
        _mockBugRepo.Setup(r => r.Get(request.BugId))
                    .ReturnsAsync(bug);

        _mockBugCommentRepo.Setup(r => r.Add(It.IsAny<BugComment>()))
                   .ReturnsAsync((BugComment bc) => bc);

        // Act
        var result = await _service.AddComment(request, email);

        // Assert
        Assert.Equal(request.Comment, result.Comment);
        Assert.Equal(request.BugId, result.BugId);
        Assert.Equal(employee.EmployeeId, result.CommenterId);
    }

    [Fact]
    public async Task GetAllCommentsForBug_ReturnsComments()
    {
        // Arrange
        long bugId = 1;
        var comments = new List<BugComment>
        {
            new BugComment { BugId = bugId, Comment = "Comment 1" },
            new BugComment { BugId = bugId, Comment = "Comment 2" }
        };

        _mockBugCommentRepo.Setup(r => r.GetAll())
                           .ReturnsAsync(comments);

        // Act
        var result = await _service.GetAllCommentsForBug(bugId);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains("Comment 1", result);
        Assert.Contains("Comment 2", result);
    }

    [Fact]
    public async Task DeleteComments_AdminCanDelete_ReturnsSuccess()
    {
        // Arrange
        long commentId = 1;
        string email = "admin@example.com";
        string role = "Admin";
        var employee = new Employee { EmployeeId = 100 };
        var comment = new BugComment { CommentId = commentId, CommenterId = 100 };

        _mockEmployeeRepo.Setup(r => r.GetEmployeeByEmail(email)).ReturnsAsync(employee);
        _mockBugCommentRepo.Setup(r => r.Get(commentId)).ReturnsAsync(comment);
        _mockBugCommentRepo.Setup(r => r.Delete(commentId)).ReturnsAsync(comment);

        // Act
        var result = await _service.DeleteComments(commentId, email, role);

        // Assert
        Assert.Equal("Comment Deleted Successfully", result);
    }

    [Fact]
    public async Task DeleteComments_CommenterCanDelete_ReturnsSuccess()
    {
        // Arrange
        long commentId = 1;
        string email = "user@example.com";
        string role = "Dev";
        var employee = new Employee { EmployeeId = 101 };
        var comment = new BugComment { CommentId = commentId, CommenterId = 101 };

        _mockEmployeeRepo.Setup(r => r.GetEmployeeByEmail(email)).ReturnsAsync(employee);
        _mockBugCommentRepo.Setup(r => r.Get(commentId)).ReturnsAsync(comment);
        _mockBugCommentRepo.Setup(r => r.Delete(commentId)).ReturnsAsync(comment);

        // Act
        var result = await _service.DeleteComments(commentId, email, role);

        // Assert
        Assert.Equal("Comment Deleted Successfully", result);
    }

    [Fact]
    public async Task DeleteComments_InvalidComment_ThrowsNotFound()
    {
        long commentId = 1;
        string email = "user@example.com";
        string role = "Dev";

        _mockEmployeeRepo.Setup(r => r.GetEmployeeByEmail(email)).ReturnsAsync(new Employee { EmployeeId = 1 });
        _mockBugCommentRepo.Setup(r => r.Get(commentId)).ReturnsAsync((BugComment)null);

        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteComments(commentId, email, role));
        Assert.Contains("not found", ex.Message);
    }

    [Fact]
    public async Task DeleteComments_Unauthorized_ThrowsException()
    {
        long commentId = 1;
        string email = "user@example.com";
        string role = "Dev";

        _mockEmployeeRepo.Setup(r => r.GetEmployeeByEmail(email)).ReturnsAsync(new Employee { EmployeeId = 100 });
        _mockBugCommentRepo.Setup(r => r.Get(commentId)).ReturnsAsync(new BugComment { CommentId = 1, CommenterId = 999 });

        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.DeleteComments(commentId, email, role));
        Assert.Equal("Not authorized", ex.Message);
    }
}
