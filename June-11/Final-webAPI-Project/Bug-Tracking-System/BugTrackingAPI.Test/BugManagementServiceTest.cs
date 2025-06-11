using Xunit;
using Moq;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackingAPI.Tests.UnitTests
{
    public class BugManagementServiceTest
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
        private readonly Mock<IRepository<long, Bugs>> _mockBugsRepo;
        private readonly Mock<IRepository<long, BugAssignment>> _mockBugAssignmentRepo;
        private readonly BugManagementService _service;

        public BugManagementServiceTest()
        {
            _mockEmployeeRepo = new Mock<IEmployeeRepository>();
            _mockBugsRepo = new Mock<IRepository<long, Bugs>>();
            _mockBugAssignmentRepo = new Mock<IRepository<long, BugAssignment>>();

            _service = new BugManagementService(
                _mockEmployeeRepo.Object,
                _mockBugsRepo.Object,
                _mockBugAssignmentRepo.Object
            );
        }

        [Fact]
        public async Task AssignBugToDevs_Successfully_AssignsBug()
        {
            // Arrange
            long bugId = 1, devId = 2;
            string email = "admin@example.com";
            var dev = new Employee { EmployeeId = devId, Role = "Dev" };
            var bug = new Bugs
            {
                BugId = bugId,
                Status = "Open",
                BugName = "XSS Bug",
                Description = "desc",
                Screenshot = "",
                CvssScore = 5.0f,
                SubmittedOn = DateTime.UtcNow,
                SubmittedById = 1
            };
            var assignments = new List<BugAssignment>();

            _mockBugAssignmentRepo.Setup(r => r.GetAll())
                .ReturnsAsync(assignments);
            _mockBugsRepo.Setup(r => r.Get(bugId))
                .ReturnsAsync(bug);
            _mockEmployeeRepo.Setup(r => r.Get(devId))
                .ReturnsAsync(dev);
            _mockEmployeeRepo.Setup(r => r.GetEmployeeByEmail(email))
                .ReturnsAsync(new Employee { EmployeeId = 999 });

            _mockBugAssignmentRepo.Setup(r => r.Get(bugId))
            .ReturnsAsync(new BugAssignment { 
                BugId = bugId, 
                DevId = 123, 
                ResolutionStatus = "ongoing", 
                AssignedDate = DateTime.UtcNow, 
                AssignedById = 1 
            });

            // Act
            var result = await _service.AssignBugToDevs(bugId, devId, email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bugId, result.BugId);
            Assert.Equal(devId, result.DevId);
            Assert.Equal("ongoing", result.ResolutionStatus);
            Assert.Equal("XSS Bug", result.Bug.BugName);
        }

        [Fact]
        public async Task AssignBugToDevs_Throws_When_BugAlreadyAssigned()
        {
            // Arrange
            long bugId = 1, devId = 2;
            string email = "admin@example.com";

            _mockBugAssignmentRepo.Setup(r => r.GetAll())
                .ReturnsAsync(new List<BugAssignment> {
                    new BugAssignment { BugId = bugId, DevId = devId }
                });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _service.AssignBugToDevs(bugId, devId, email));

            Assert.Equal("Bug Already Assigned", ex.Message);
        }

        

        [Fact]
        public async Task ResolveBugs_Successfully_ResolvesBug()
        {
            // Arrange
            long bugId = 1;
            string email = "dev@example.com";
            var dev = new Employee { EmployeeId = 123, Role = "Dev" };
            var bug = new Bugs
            {
                BugId = bugId,
                BugName = "UI Bug",
                CvssScore = 3.5f,
                Description = "desc",
                SubmittedOn = DateTime.UtcNow,
                Status = "Open",
                SubmittedById = 5
            };
            var assignment = new BugAssignment
            {
                BugId = bugId,
                DevId = 123,
                AssignedById = 1,
                AssignedDate = DateTime.UtcNow,
                ResolutionStatus = "ongoing"
            };

            _mockEmployeeRepo.Setup(r => r.GetEmployeeByEmail(email))
                .ReturnsAsync(dev);
         

            _mockBugAssignmentRepo.Setup(r => r.Get(bugId))
                .ReturnsAsync(assignment);
            _mockBugAssignmentRepo.Setup(r => r.Get(bugId))
            .ReturnsAsync(new BugAssignment { 
                BugId = bugId, 
                DevId = 123, 
                ResolutionStatus = "ongoing", 
                AssignedDate = DateTime.UtcNow, 
                AssignedById = 1 
            });
            _mockBugsRepo.Setup(r => r.Get(bugId))
            .ReturnsAsync(new Bugs {
                BugId = bugId,
                BugName = "UI Bug",
                CvssScore = 3.5f,
                Description = "desc",
                SubmittedOn = DateTime.UtcNow,
                Status = "Open",
                SubmittedById = 5
            });


            // Act
            var result = await _service.ResolveBugs(bugId, email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UI Bug", result.Bug.BugName);
        }
    }
}
