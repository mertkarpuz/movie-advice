using MovieAdvice.Domain.Models;


namespace MovieAdvice.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        bool CheckPassword(string password, string userPassword);
    }
}
