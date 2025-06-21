using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugTrackingAPI.Services;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Linq;

namespace BugTrackingAPI.Test.Services
{
    public class BugServiceTest
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
        private readonly Mock<IRepository<long, Bugs>> _mockBugsRepo;
        private readonly BugService _service;

        public BugServiceTest()
        {
            _mockEmployeeRepo = new Mock<IEmployeeRepository>();
            _mockBugsRepo = new Mock<IRepository<long, Bugs>>();
            _service = new BugService(_mockEmployeeRepo.Object, _mockBugsRepo.Object);
        }

        [Fact]
        public async Task GetAllBugsList_ReturnsMappedBugResponses()
        {
            // Arrange
            var bugs = new List<Bugs>
            {
                new Bugs
                {
                    BugId = 1,
                    BugName = "SQL Injection",
                    Description = "Critical DB flaw",
                    Screenshot = "path1",
                    CvssScore = 9.8F,
                    SubmittedOn = DateTime.UtcNow,
                    Status = "ongoing",
                    SubmittedById = 101
                }
            };
            _mockBugsRepo.Setup(r => r.GetAll()).ReturnsAsync(bugs);

            // Act
            var result = await _service.GetAllBugsList();

            // Assert
            Assert.Single(result);
            Assert.Equal("SQL Injection", result.First().BugName);
        }

        [Fact]
        public async Task GetBugStatus_ValidId_ReturnsBug()
        {
            // Arrange
            var bug = new Bugs
            {
                BugId = 1,
                BugName = "Null Ref",
                Description = "Crash issue",
                Screenshot = "screenshot.png",
                CvssScore = 6.5F,
                SubmittedOn = DateTime.UtcNow,
                Status = "ongoing",
                SubmittedById = 100
            };
            _mockBugsRepo.Setup(r => r.Get(1)).ReturnsAsync(bug);

            // Act
            var result = await _service.GetBugStatus(1);

            // Assert
            Assert.Equal(1, result.BugId);
            Assert.Equal("Null Ref", result.BugName);
        }

        [Fact]
        public async Task GetBugStatus_InvalidId_ThrowsException()
        {
            _mockBugsRepo.Setup(r => r.Get(It.IsAny<long>())).ReturnsAsync((Bugs)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetBugStatus(123));
        }

        [Fact]
        public async Task SubmitBug_SavesBugToRepo_ReturnsBugDto()
        {
            // Arrange: mock IFormFile
            var content = "fake file content";
            var fileName = "exploit.png";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            var formFile = new FormFile(stream, 0, stream.Length, "screenshot", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };

            var bugDto = new BugSubmission
            {
                BugName = "XSS",
                Description = "Script injection",
                Screenshot = formFile,
                CvssScore = 7.2F
            };

            _mockEmployeeRepo.Setup(e => e.GetEmployeeByEmail("test@example.com"))
                             .ReturnsAsync(new Employee { EmployeeId = 555 });

            _mockBugsRepo.Setup(r => r.Add(It.IsAny<Bugs>()))
                         .ReturnsAsync((Bugs b) => b);

            // Act
            var result = await _service.SubmitBug(bugDto, "test@example.com");

            // Assert
            Assert.Equal("XSS", result.BugName);
            Assert.Equal("Script injection", result.Description);
        }
    }
}
