using System.Collections.Generic;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Scraper.App.Providers
{
    public interface IApiProvider
    {
        Task<IEnumerable<Character>> GetCastPerShowIdAsync(long showId);
        Task<IEnumerable<Show>> GetShowsPerPageAsync(int pageNumber = 0);
    }
}