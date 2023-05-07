using MovieAdvice.Domain.ApiModels;


namespace MovieAdvice.Application.Interfaces
{
    public interface IGetMoviesService
    {
        Task<RootApiModel> GetMovies(int page);
    }
}
