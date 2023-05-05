using Microsoft.EntityFrameworkCore;
using MovieAdvice.Domain.Interfaces;
using MovieAdvice.Domain.Models;
using MovieAdvice.Infrastructure.Context;

namespace MovieAdvice.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAdviceDbContext context;
        public MovieRepository(MovieAdviceDbContext context)
        {
            this.context = context;
        }

        public void SaveMovies(List<Movie> movieList)
        {
            context.Movies.AddRange(movieList);
            context.SaveChanges();
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            return await context.Movies.Include(x => x.Comments)
                .ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == movieId);
        }
        public async void UpdateMoviesStatus()
        {
            await context.Movies.Where(x=>x.Status == true).ForEachAsync(x => x.Status = false);
        }
        public async Task<List<Movie>> GetActiveMovies() // pagination
        {
            return await context.Movies.Where(x => x.Status == true).ToListAsync();
        }
    }
}
