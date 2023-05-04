using MovieAdvice.Application.Interfaces;

namespace GetMoviesWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IGetMoviesService getMoviesService;
        public Worker(ILogger<Worker> logger, IGetMoviesService getMoviesService)
        {
            _logger = logger;
            this.getMoviesService = getMoviesService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int? totalPage = 0;
            int currentPage = 1;

            var rootApiModel = await getMoviesService.GetMovies(currentPage); //id.

            if (rootApiModel != null)
            {
                totalPage = rootApiModel.TotalPages;
            }
            while (totalPage > currentPage)
            {
                currentPage++;
                rootApiModel = await getMoviesService.GetMovies(currentPage);

                if (rootApiModel != null && rootApiModel.Movies != null)
                {
                    _logger.LogInformation(currentPage.ToString());
                }
            }



        }
    }
}