using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.ApiModels;


namespace MovieAdvice.Application.Services
{
    public class GetMoviesService : IGetMoviesService
    {
        private readonly IHttpUtilities httpUtilities;
        private readonly Configuration configuration;
        public GetMoviesService(IHttpUtilities httpUtilities,Configuration configuration)
        {
            this.httpUtilities = httpUtilities;
            this.configuration = configuration;
        }
        public async Task<RootApiModel> GetMovies(int page)
        {
            RootApiModel rootApiModel = new();
            string response = await httpUtilities.ExecuteGetHttpRequest(configuration.MovieApiConfigurations.ApiUrl +
                "?api_key=" + configuration.MovieApiConfigurations.ApiKey +
                "&language=" + configuration.MovieApiConfigurations.ApiLanguage + "&page=" + page, null);
            if (!string.IsNullOrEmpty(response))
            {
                rootApiModel = System.Text.Json.JsonSerializer.Deserialize<RootApiModel>(response);
            }
            return rootApiModel;
        }
    }
}
