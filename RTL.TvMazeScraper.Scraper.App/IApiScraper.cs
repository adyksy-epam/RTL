using System.Threading;
using System.Threading.Tasks;

namespace RTL.TvMazeScraper.Scraper.App
{
    public interface IApiScraper
    {
        Task ScrapeApiAsync(CancellationToken token);
    }
}