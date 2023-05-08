using Cronos;
using MovieAdvice.Application.Interfaces;
using MovieAdvice.Domain.ApiModels.MovieApi;

namespace GetMoviesWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IGetMoviesService getMoviesService;
        private const string schedule = "0 * * * *"; // every hour
        private readonly CronExpression cron;
        private readonly IServiceProvider Services;
        public Worker(ILogger<Worker> logger, IServiceProvider services, IGetMoviesService getMoviesService)
        {
            _logger = logger;
            this.getMoviesService = getMoviesService;
            Services = services;
            cron = CronExpression.Parse(schedule);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("----Worker running at: {time} ----", DateTimeOffset.Now);
                DateTime utcNow = DateTime.UtcNow;
                DateTime? nextUtc = cron.GetNextOccurrence(utcNow);
                await Task.Delay(nextUtc.Value - utcNow, stoppingToken);
                await DoBackupAsync();
            }
        }

        public async Task DoBackupAsync()
        {
            try
            {
                IServiceScope scope = Services.CreateScope();
                IMoviesService moviesService = scope.ServiceProvider.GetRequiredService<IMoviesService>();
                int? totalPage = 0;
                int currentPage = 1;
                List<MovieApiModel> movieList = new();

                moviesService.UpdateMoviesStatus();

                var rootApiModel = await getMoviesService.GetMovies(currentPage);

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
                            movieList.Add(movie);
                        }
                    }
                    currentPage++;
                    rootApiModel = await getMoviesService.GetMovies(currentPage);
                }

                moviesService.SaveMovie(movieList);

                _logger.LogInformation("---- DONE ---- ");

                scope.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
            }
        }
    }
}