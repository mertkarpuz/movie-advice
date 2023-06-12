using Microsoft.Extensions.DependencyInjection;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.ApiModels;
using MovieAdvice.Domain.ApiModels.MovieApi;

namespace MovieAdvice.Application.Services
{
    public class GetMoviesService : IGetMoviesService
    {
        private readonly IHttpUtilities httpUtilities;
        private readonly Configuration configuration;
        private readonly IMoviesService moviesService;
        public GetMoviesService(IHttpUtilities httpUtilities,Configuration configuration, IMoviesService moviesService)
        {
            this.httpUtilities = httpUtilities;
            this.configuration = configuration;
            this.moviesService = moviesService;
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


        public async Task SyncMovies()
        {
            int? totalPage = 0;
            int currentPage = 1;
            List<MovieApiModel> movieList = new();

            var rootApiModel = await GetMovies(currentPage);

            if (rootApiModel != null)
            {
                totalPage = rootApiModel.TotalPages;
            }

            while (totalPage >= currentPage)
            {
                if (rootApiModel != null && rootApiModel.Movies != null)
                {

                    foreach (var movie in rootApiModel.Movies)
                    {
                        var isExists = await moviesService.GetMovieByTitle(movie.Title);
                        if (isExists == null)
                        {
                            movieList.Add(movie);
                        }
                    }
                }

                currentPage++;
                rootApiModel = await GetMovies(currentPage);
            }

            if (movieList.Count > 0)
            {
                moviesService.SaveMovie(movieList);
            }
        }
    }
}
