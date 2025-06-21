using Xunit;
using Moq;
using BugTrackingAPI.Controllers;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using BugTrackingAPI.Hubs;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace BugTrackingAPI.Tests.UnitTests
{
    public class BugsControllerTest
    {
        private readonly Mock<IBugService> _mockBugService;
        private readonly Mock<IHubContext<NotificationHub>> _mockHubContext;
        private readonly Mock<IClientProxy> _mockClientProxy;
        private readonly BugsController _controller;

        public BugsControllerTest()
        {
            _mockBugService = new Mock<IBugService>();
            _mockHubContext = new Mock<IHubContext<NotificationHub>>();
            _mockClientProxy = new Mock<IClientProxy>();

            var clients = new Mock<IHubClients>();
            clients.Setup(c => c.All).Returns(_mockClientProxy.Object);
            _mockHubContext.Setup(h => h.Clients).Returns(clients.Object);

            _controller = new BugsController(_mockBugService.Object, _mockHubContext.Object);
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
        public async Task ListAllReportedBugs_ReturnsOkResult_WithBugList()
        {
            // Arrange
            var mockBugs = new List<BugResponse>
            {
                new BugResponse
                {
                    BugId = 1,
                    BugName = "Null Reference Exception",
                    Description = "Occurs when clicking submit with no input.",
                    Screenshot = "screenshot1.png",
                    CvssScore = 7.5f,
                    SubmittedOn = DateTime.UtcNow.AddDays(-2),
                    ResolvedAt = null,
                    Status = "ongoing",
                    SubmittedById = 1001
                },
                new BugResponse
                {
                    BugId = 2,
                    BugName = "SQL Injection",
                    Description = "Detected in login form input.",
                    Screenshot = "screenshot2.png",
                    CvssScore = 9.1f,
                    SubmittedOn = DateTime.UtcNow.AddDays(-1),
                    ResolvedAt = DateTime.UtcNow,
                    Status = "Resolved",
                    SubmittedById = 1002
                }
            };

            _mockBugService.Setup(service => service.GetAllBugsList())
                        .ReturnsAsync(mockBugs);

            // Act
            var result = await _controller.ListAllReportedBugs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<BugResponse>>(okResult.Value);

            var bugs = new List<BugResponse>(returnValue);
            Assert.Equal(2, bugs.Count);
            Assert.Equal("SQL Injection", bugs[1].BugName);
            Assert.Equal("Resolved", bugs[1].Status);
            Assert.True(bugs[0].CvssScore < bugs[1].CvssScore);
        }


    }
}
