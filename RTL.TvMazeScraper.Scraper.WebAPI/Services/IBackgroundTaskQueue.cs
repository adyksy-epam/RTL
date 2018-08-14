using System.Threading;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Scraper.WebAPI.Commands;

namespace RTL.TvMazeScraper.Scraper.WebAPI.Services
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(ScrapeApiCommand workItem);

        Task<ScrapeApiCommand> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
