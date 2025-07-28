
using ChienShopMigrationProject.Interfaces;
using ChienShopMigrationProject.Repositories;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;

namespace ChienVHShopOnline.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepository;

        public UserService(IRepository<int, User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(RegisterUserDto user)
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

            return newUser;
        }

        public async Task<User> Login(RegisterUserDto user)
        {
            var allUsers = await _userRepository.GetAll();
            bool check = allUsers.Any(e => e.Username == user.Username && e.Password == user.Password);

            if (check)
            {
                var newUserEntity = new User
                {

                    Username = user.Username,
                    Password = user.Password

                };

                var newUser = await _userRepository.Add(newUserEntity);

                return newUser;
            }
            else
            {
                throw new Exception("Invalid Credentials");
            }
        }

        
    }
}