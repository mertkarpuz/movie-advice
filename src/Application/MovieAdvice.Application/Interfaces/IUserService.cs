using MovieAdvice.Domain.Models;


namespace MovieAdvice.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        bool CheckPassword(string password, string userPassword);
    }
}
