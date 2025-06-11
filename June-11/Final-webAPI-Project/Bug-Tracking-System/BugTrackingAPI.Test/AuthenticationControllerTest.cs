using Xunit;
using Moq;
using BugTrackingAPI.Controllers;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BugTrackingAPI.Tests.UnitTests
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockTokenService = new Mock<ITokenService>();

            _controller = new AuthenticationController(
                _mockAuthenticationService.Object,
                _mockTokenService.Object
            );
        }

        //RegisterUser Tests

        [Fact]
        public async Task RegisterUser_ReturnsCreated_OnSuccessfulRegistration()
        {
            // Arrange
            var regRequest = new UserRegisterRequest { Name = "testing", Email = "test@example.com", Password = "Password123!", Role = "Admin" };
            var demoDate = DateTime.UtcNow;
            var expectedResponse = new UserLoginResponse { Email = "test@example.com", AccessToken = "fake_token", RefreshToken = "fake_refresh_token", RefreshTokenExpiryTime = demoDate };

            _mockAuthenticationService.Setup(s => s.Register(regRequest))
                                      .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.RegisterUser(regRequest);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
            var actualResponse = Assert.IsType<UserLoginResponse>(createdResult.Value);
            Assert.Equal(expectedResponse.Email, actualResponse.Email);  //change email to token or something to simulate a fail case
            Assert.Equal(expectedResponse.AccessToken, actualResponse.AccessToken);

            _mockAuthenticationService.Verify(s => s.Register(regRequest), Times.Once);
        }


        [Fact] //login tests
        public async Task UserLogin_ReturnsOk_OnSuccessfulLogin()
        {
            // Arrange
            var loginRequest = new UserLoginRequest { Email = "user@example.com", Password = "Password123!" };
            var demoDate = DateTime.UtcNow;
            var expectedResponse = new UserLoginResponse { Email = "users@example.com", AccessToken = "fake_token", RefreshToken = "fake_refresh_token", RefreshTokenExpiryTime = demoDate };

            _mockAuthenticationService.Setup(s => s.Login(loginRequest))
                                      .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.UserLogin(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var actualResponse = Assert.IsType<UserLoginResponse>(okResult.Value);
            Assert.Equal(expectedResponse.AccessToken, actualResponse.AccessToken);
            Assert.Equal(expectedResponse.Email, actualResponse.Email);

            _mockAuthenticationService.Verify(s => s.Login(loginRequest), Times.Once);
        }

       [Fact]
        public async Task UserLogin_ReturnsUnauthorized_OnInvalidCredentials() //invalid creds
        {
            // Arrange
            var loginRequest = new UserLoginRequest
            {
                Email = "invalid@example.com",
                Password = "WrongPassword!"
            };

            _mockAuthenticationService.Setup(s => s.Login(loginRequest))
                                    .ReturnsAsync((UserLoginResponse?)null);

            // Act
            var result = await _controller.UserLogin(loginRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
            Assert.Equal("Invalid email or password.", unauthorizedResult.Value);

            _mockAuthenticationService.Verify(s => s.Login(loginRequest), Times.Once);
        }


       
    }
}
