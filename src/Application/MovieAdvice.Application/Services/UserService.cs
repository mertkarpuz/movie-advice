using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public bool CheckPassword(string password, string userPassword)
        {
            return userRepository.CheckPassword(password, userPassword);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await userRepository.GetUserByEmail(email);
        }
    }
}
