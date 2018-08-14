using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Data.Context;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Data.Repository
{
    public class Repository : ReadOnlyRepository, IRepository
    {
        public Repository(TvMazeDbContext context) : base(context)
        {
        }

        public async Task AddOrUpdateShowAsync(Show entity)
        {
            if (IsAttached(entity))
            {
                _context.Set<Show>().Update(entity);
            }
            else
            {
                await _context.Set<Show>().AddAsync(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool IsAttached(Show show) => _context.Set<Show>().Local.Any(s => s == show);
    }
}
