using Xunit;
using Moq;
using BugTrackingAPI.Controllers;
using BugTrackingAPI.Hubs;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace BugTrackingAPI.Tests.UnitTests
{
    public class BugCommentControllerTests
    {
        private readonly Mock<IBugService> _mockBugService;
        private readonly Mock<IBugCommentService> _mockBugCommentService;
        private readonly Mock<IHubContext<NotificationHub>> _mockHubContext;
        private readonly BugCommentController _controller;

        public BugCommentControllerTests()
        {
            _mockBugService = new Mock<IBugService>();
            _mockBugCommentService = new Mock<IBugCommentService>();
            _mockHubContext = new Mock<IHubContext<NotificationHub>>();

            _controller = new BugCommentController(
                _mockBugService.Object,
                _mockHubContext.Object,
                _mockBugCommentService.Object
            );
        }

        private void SetUserEmail(string email)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task AddComment_ThrowsException_WhenUserNotLoggedIn()
        {
            // Arrange
            var request = new BugCommentRequest { BugId = 1, Comment = "Test comment" };
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _controller.AddComment(request));
            Assert.Equal("Not logged in", ex.Message);
        }
        
        [Fact]
        public async Task GetAllComments_ReturnsOkResult_WithCommentList()
        {
            // Arrange
            var bugId = 42L;
            var comments = new List<string>
            {
                "Comment 1",
                "Comment 2"
            };

            _mockBugCommentService.Setup(s => s.GetAllCommentsForBug(bugId))
                                  .ReturnsAsync(comments);

            // Act
            var result = await _controller.GetAllComments(bugId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
            Assert.Equal(2, ((List<string>)value).Count);
        }


    }
}