
using ChienShopMigrationProject.Interfaces;
using ChienShopMigrationProject.Repositories;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;

namespace ChienVHShopOnline.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<int, User> userRepository,
                            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserLoginResponse> Register(RegisterUserDto user)
        {
            var allUsers = await _userRepository.GetAll();
            bool check = allUsers.Any(e => e.Username == user.Username);
            if (check)
            {
                throw new Exception("Username Already Taken");
            }
            var newUserEntity = new User
            {

                Username = user.Username,
                Password = user.Password

            };

            var newUser = await _userRepository.Add(newUserEntity);

             var token = await _tokenService.GenerateToken(newUser);

            return new UserLoginResponse
            {
                Email = user.Username,
                AccessToken = token.AccessToken
            };


        }

        public async Task<UserLoginResponse> Login(RegisterUserDto user)
        {
            var allUsers = await _userRepository.GetAll();
            bool check = allUsers.Any(e => e.Username == user.Username && e.Password == user.Password);

            if (check)
            {
                var userData = allUsers.FirstOrDefault(e => e.Username == user.Username && e.Password == user.Password);

                // var newUser = await _userRepository.Add(newUserEntity);
                var userlogin = new User
                {
                    Username = user.Username,
                    Password = user.Password
                };

                 var token = await _tokenService.GenerateToken(userlogin);

                return new UserLoginResponse
                {
                    Email = user.Username,
                    AccessToken = token.AccessToken
                };
            }
            else
            {
                throw new Exception("Invalid Credentials");
            }
        }

        
    }
}