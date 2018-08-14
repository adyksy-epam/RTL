using System.Collections.Generic;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Data.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        Task AddOrUpdateShowAsync(Show entity);
        Task SaveChangesAsync();
    }
}
