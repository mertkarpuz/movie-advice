using Cronos;
using Microsoft.Extensions.Logging;
using MovieAdvice.Application.Interfaces;

namespace GetMoviesWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IGetMoviesService getMoviesService;
        private const string schedule = "* * * * *"; // every hour
        private readonly CronExpression cron;
        public Worker(ILogger<Worker> logger, IGetMoviesService getMoviesService)
        {
            _logger = logger;
            this.getMoviesService = getMoviesService;
            cron = CronExpression.Parse(schedule);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("-------------Worker running at: {time}", DateTimeOffset.Now);
                var utcNow = DateTime.UtcNow;
                DateTime? nextUtc = cron.GetNextOccurrence(utcNow);
                await Task.Delay(nextUtc.Value - utcNow, stoppingToken);
                await DoBackupAsync();

            }


        }

        public async Task DoBackupAsync()
        {
            try
            {
                int? totalPage = 0;
                int currentPage = 1;
                int test = 1;

                var rootApiModel = await getMoviesService.GetMovies(currentPage);

                if (rootApiModel != null)
                {
                    totalPage = rootApiModel.TotalPages;
                }
                while (totalPage >= currentPage)
                {
                    // addDb
                    if (rootApiModel != null && rootApiModel.Movies != null)
                    {
                        
                        foreach (var item in rootApiModel.Movies)
                        {
                            _logger.LogInformation(item.OriginalTitle);
                            Thread.Sleep(10);
                        }
                    }

                    currentPage++;
                    rootApiModel = await getMoviesService.GetMovies(currentPage);

                    
                }
                _logger.LogInformation("total " + test);
            }
            catch (Exception ex)
            {
                _logger.LogError("",ex);
            }
            
        }
    }
}