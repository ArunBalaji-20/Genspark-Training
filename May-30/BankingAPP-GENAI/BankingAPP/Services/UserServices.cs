using BankingAPP.DTOs;
using BankingAPP.Interfaces;
using BankingAPP.Models;
using BankingAPP.Repositories;

namespace BankingAPP.Services
{
    public class UserServices : IUserService
    {
        private readonly IRepository<int, User> _userRepository;
        public UserServices(IRepository<int, User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> AddUser(UserAddRequestDto user_dto)
        {
            int lastAccountNumber = await GetLastAccountNumber();
            int newAccountNumber = lastAccountNumber + 3;

            var user = new User
            {
                AccountNumber = newAccountNumber,
                Name = user_dto.Name,
                Age = user_dto.Age,
                Balance = user_dto.InitialDeposit
            };

            // Step 3: Save via repository
            var createdUser = await _userRepository.Add(user);
            return createdUser;
        }
        
        private async Task<int> GetLastAccountNumber()
        {
            var allUsers = await _userRepository.GetAll();
            if (!allUsers.Any())
                return 100001; 

            return allUsers.Max(u => u.AccountNumber);
        }
    }
}