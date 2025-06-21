using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugTrackingAPI.Services;
using BugTrackingAPI.Models;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models.DTO;
using System.Linq;
using System;

namespace BugTrackingAPI.Tests
{
    public class EmployeeManagementServiceTests
    {
        private readonly Mock<IRepository<long, Employee>> _mockRepo;
        private readonly EmployeeManagementService _service;

        public EmployeeManagementServiceTests()
        {
            _mockRepo = new Mock<IRepository<long, Employee>>();
            _service = new EmployeeManagementService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsMappedEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = 1, Name = "Alice", Email = "alice@example.com", Role = "Developer" },
                new Employee { EmployeeId = 2, Name = "Bob", Email = "bob@example.com", Role = "Tester" }
            };

            _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(employees);

            // Act
            var result = await _service.GetAllEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.Name == "Alice" && e.Role == "Developer");
            Assert.Contains(result, e => e.Email == "bob@example.com");
        }

        [Fact]
        public async Task DeleteEmployee_ValidId_ReturnsSuccessMessage()
        {
            // Arrange
            var employee = new Employee { EmployeeId = 1, Name = "Charlie" };

            _mockRepo.Setup(r => r.Delete(1)).ReturnsAsync(employee);

            // Act
            var result = await _service.DeleteEmployee(1);

            // Assert
            Assert.Equal("Employee Deleted Successfully", result);
        }

        [Fact]
        public async Task DeleteEmployee_InvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.Delete(99)).ReturnsAsync((Employee)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteEmployee(99));
            Assert.Equal("Employee not found or already deleted.", ex.Message);
        }
    }
}
