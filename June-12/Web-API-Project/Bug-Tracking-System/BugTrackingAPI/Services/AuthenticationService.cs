
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Models.DTOs;

namespace BugTrackingAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IEncryptionService _encryptionService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRepository<long,BlackListedToken> _blackListRepository;

        public AuthenticationService(ITokenService tokenService,
                                    IEncryptionService encryptionService,
                                    IEmployeeRepository employeeRepository,
                                    IRefreshTokenRepository refreshTokenRepository,
                                    IRepository<long,BlackListedToken> blackListRepository)
        {
            _tokenService = tokenService;
            _encryptionService = encryptionService;
            _employeeRepository = employeeRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _blackListRepository = blackListRepository;
        }
        public async Task<UserLoginResponse> Register(UserRegisterRequest UserReg)
        {
            var existingUser = await _employeeRepository.GetEmployeeByEmail(UserReg.Email);
            if (existingUser != null)
            {
                throw new Exception("Username already taken");
            }

            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = UserReg.Password
            });


            var newUser = new Employee
            {
                Name = UserReg.Name,
                Email = UserReg.Email,
                PasswordHash = encryptedData.EncryptedData,
                Role = UserReg.Role
            };

            var addedUser = await _employeeRepository.Add(newUser);
            if (addedUser == null)
            {
                throw new Exception("User registration failed");
            }

            var token = await _tokenService.GenerateToken(addedUser);

            return new UserLoginResponse
            {
                Email = addedUser.Email,
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                RefreshTokenExpiryTime = token.RefreshTokenExpiryTime
            };

        }
        public async Task<UserLoginResponse> Login(UserLoginRequest user)
        {
            var dbUser = await _employeeRepository.GetEmployeeByEmail(user.Email);
            if (dbUser == null)
            {
                throw new Exception("Invalid Email/password");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }

            var token = await _tokenService.GenerateToken(dbUser);

            return new UserLoginResponse
            {
                Email = user.Email,
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                RefreshTokenExpiryTime = token.RefreshTokenExpiryTime
            };
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {

            var storedToken = await _refreshTokenRepository.GetByToken(refreshToken);
            if (storedToken == null || storedToken.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired refresh token");
            }

            var employee = await _employeeRepository.Get(storedToken.EmployeeId);
            if (employee == null)
            {
                throw new Exception("Invalid or expired refresh token");

            }

            var newTokens = await _tokenService.GenerateToken(employee);

            await _refreshTokenRepository.Delete(storedToken.Id);


            return newTokens;
        }

        public async Task<string> LogOut(LogOutRequest request,string accessToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                throw new ArgumentException("Refresh token is required for logout.");
            }

            var storedToken = await _refreshTokenRepository.GetByToken(request.RefreshToken);
            if (storedToken == null)
            {
                throw new Exception("Invalid refresh token");
            }
            await _refreshTokenRepository.Delete(storedToken.Id);
            var jwtExpiry = _tokenService.GetExpiryFromToken(accessToken); 
            await _blackListRepository.Add(new BlackListedToken
            {
                Token = accessToken,
                ExpiryDate = jwtExpiry
            });
            return "Logged out successfully";


        }
        
    
    }
}