using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Scraper.WebAPI.Commands;

namespace RTL.TvMazeScraper.Scraper.WebAPI.Services
{
    public class ScraperTaskQueue : IBackgroundTaskQueue
    {
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);
        private readonly ConcurrentQueue<ScrapeApiCommand> _workItems =
            new ConcurrentQueue<ScrapeApiCommand>();

        public void QueueBackgroundWorkItem(ScrapeApiCommand workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<ScrapeApiCommand> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
