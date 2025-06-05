
using DocNotifyAPI.Interfaces;
using DocNotifyAPI.Models;
using DocNotifyAPI.Models.DTO;
using Microsoft.Extensions.Logging;

namespace DocNotifyAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<string, User> _userRepository;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ITokenService tokenService,
                                    IEncryptionService encryptionService,
                                    IRepository<string, User> userRepository,
                                    ILogger<AuthenticationService> logger)
        {
            _tokenService = tokenService;
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<UserLoginResponse> Register(UserRegisterRequest UserReg)
        {
            var existingUser = await _userRepository.Get(UserReg.Username);
            if (existingUser != null)
            {
                _logger.LogWarning("User already exists");
                throw new Exception("Username already taken");
            }

            // 2. Generate a new hash key
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
                {
                    Data= UserReg.Password
                });
                

            var newUser = new User
            {
                Username = UserReg.Username,
                Password = encryptedData.EncryptedData,
                HashKey = encryptedData.HashKey,
                Role = UserReg.Role
            };

            var addedUser = await _userRepository.Add(newUser);
            if (addedUser == null)
            {
                _logger.LogError("User registration failed while saving user");
                throw new Exception("User registration failed");
            }

            var token = await _tokenService.GenerateToken(addedUser);

            return new UserLoginResponse
            {
                Username = addedUser.Username,
                Token = token
            };

        }
        public async Task<UserLoginResponse> Login(UserLoginRequest user)
        {
            var dbUser = await _userRepository.Get(user.Username);
            if (dbUser == null)
            {
                _logger.LogCritical("User not found");
                throw new Exception("No such user");
            }
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = user.Password,
                HashKey = dbUser.HashKey
            });
            for (int i = 0; i < encryptedData.EncryptedData.Length; i++)
            {
                if (encryptedData.EncryptedData[i] != dbUser.Password[i])
                {
                    _logger.LogError("Invalid login attempt");
                    throw new Exception("Invalid password");
                }
            }
            // dbUser.Role = "Patient";
            var token = await _tokenService.GenerateToken(dbUser);
            return new UserLoginResponse
            {
                Username = user.Username,
                Token = token,
            };
        }
    }
}