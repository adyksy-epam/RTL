using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTL.TvMazeScraper.Data.Context;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Data.Repository
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        protected readonly TvMazeDbContext _context;

        public ReadOnlyRepository(TvMazeDbContext context)
        {
            _context = context;
        }

        public async Task<long> CountShowsAsync() => await CountShowsAsync(s => true);

        public async Task<long> CountShowsAsync(Expression<Func<Show, bool>> predicate)
        {
            return await _context.Set<Show>().CountAsync(predicate);
        }

        public async Task<IEnumerable<Show>> GetShowsAsync() => await GetShowsAsync(s => true);

        public async Task<IEnumerable<Show>> GetShowsAsync(Expression<Func<Show, bool>> predicate)
        {
            return await _context.Set<Show>()
                .Include(s => s.Cast)
                .ThenInclude(c => c.Character)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Show>> GetShowsWithPagingAsync(int pageNumber, int pageSize) => 
            await GetShowsWithPagingAsync(s => true, pageNumber, pageSize);

        public async Task<IEnumerable<Show>> GetShowsWithPagingAsync(Expression<Func<Show, bool>> predicate, int pageNumber, int pageSize)
        {
            return await _context.Set<Show>()
                .Include(s => s.Cast)
                .ThenInclude(c => c.Character)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<long> CountCharactersAsync() => await CountCharactersAsync(c => true);

        public async Task<long> CountCharactersAsync(Expression<Func<Character, bool>> predicate)
        {
            return await _context.Set<Character>().CountAsync(predicate);
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync() => await GetCharactersAsync(c => true);

        public async Task<IEnumerable<Character>> GetCharactersAsync(Expression<Func<Character, bool>> predicate)
        {
            return await _context.Set<Character>()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
