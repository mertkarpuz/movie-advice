using MovieAdvice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAdvice.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        bool CheckPassword(string password, string userPassword);
    }
}
