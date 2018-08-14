using System.Threading.Tasks;

namespace RTL.TvMazeScraper.Scraper.App.Http
{
    public interface IHttpClient
    {
        Task<string> GetAsync(string requestUrl);
    }
}
