using System.Threading.Tasks;
using RTL.TvMazeScraper.Domain.DTO;
using RTL.TvMazeScraper.Domain.Queries;

namespace RTL.TvMazeScraper.Domain.Providers
{
    public interface IShowProvider
    {
        Task<PaginatedResult<ShowDto>> GetShowsAsync(GetShowsWithCastQuery query);
    }
}