using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RTL.TvMazeScraper.Scraper.App;

namespace RTL.TvMazeScraper.Scraper.WebAPI.Services
{
    public class ScraperHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public ScraperHostedService(IBackgroundTaskQueue taskQueue,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        {
            TaskQueue = taskQueue;
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger<ScraperHostedService>();
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected override async Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Scraper Hosted Service is starting.");

            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);

                try
                {
                    if (workItem != null)
                    {
                        var scraper = _serviceProvider.CreateScope().ServiceProvider.GetService<IApiScraper>();
                        await scraper.ScrapeApiAsync(cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        $"Error occurred executing {nameof(workItem)}.");
                }
            }

            _logger.LogInformation("Scraper Hosted Service is stopping.");
        }
    }
}
