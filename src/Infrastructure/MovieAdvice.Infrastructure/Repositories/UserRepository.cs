using Microsoft.EntityFrameworkCore;
using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Domain.Models;
using MovieAdvice.Infrastructure.Context;


namespace MovieAdvice.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieAdviceDbContext context;
        public UserRepository(MovieAdviceDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public bool CheckPassword(string password, string userPassword)
        {
            if (password != userPassword)
            {
                return false;
            }
            return true;
        }
    }
}
