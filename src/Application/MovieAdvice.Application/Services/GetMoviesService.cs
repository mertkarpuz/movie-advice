using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.ApiModels;


namespace MovieAdvice.Application.Services
{
    public class GetMoviesService : IGetMoviesService
    {
        private readonly IHttpUtilities httpUtilities;
        public GetMoviesService(IHttpUtilities httpUtilities)
        {
            this.httpUtilities = httpUtilities;
        }
        public async Task<RootApiModel?> GetMovies(int page)
        {
            RootApiModel? rootApiModel = new();
            string? response = await httpUtilities.ExecuteGetHttpRequest("https://api.themoviedb.org/3/movie/now_playing?api_key=c65ceca895a683785df76dcb015143d5&language=tr-TR&page="+page, null);
            if (!string.IsNullOrEmpty(response))
            {
                rootApiModel = System.Text.Json.JsonSerializer.Deserialize<RootApiModel>(response);
            }
            return rootApiModel;
        }
    }
}
