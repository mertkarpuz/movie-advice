using Microsoft.EntityFrameworkCore;
using MovieAdvice.Domain.Models;


namespace MovieAdvice.Infrastructure.Context
{
    public class MovieAdviceDbContext : DbContext
    {
        public MovieAdviceDbContext(DbContextOptions<MovieAdviceDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
