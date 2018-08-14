using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Data.Repository
{
    public interface IReadOnlyRepository
    {
        Task<long> CountShowsAsync();
        Task<long> CountShowsAsync(Expression<Func<Show, bool>> predicate);
        Task<IEnumerable<Show>> GetShowsAsync();
        Task<IEnumerable<Show>> GetShowsAsync(Expression<Func<Show, bool>> predicate);
        Task<IEnumerable<Show>> GetShowsWithPagingAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Show>> GetShowsWithPagingAsync(Expression<Func<Show, bool>> predicate, int pageNumber, int pageSize);
        Task<long> CountCharactersAsync();
        Task<long> CountCharactersAsync(Expression<Func<Character, bool>> predicate);
        Task<IEnumerable<Character>> GetCharactersAsync();
        Task<IEnumerable<Character>> GetCharactersAsync(Expression<Func<Character, bool>> predicate);
    }
}
