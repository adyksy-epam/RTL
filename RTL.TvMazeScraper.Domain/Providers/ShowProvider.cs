using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Data.Entities;
using RTL.TvMazeScraper.Data.Repository;
using RTL.TvMazeScraper.Domain.DTO;
using RTL.TvMazeScraper.Domain.Formatters;
using RTL.TvMazeScraper.Domain.Queries;

namespace RTL.TvMazeScraper.Domain.Providers
{
    public class ShowProvider : IShowProvider
    {
        private readonly IReadOnlyRepository _repository;
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public ShowProvider(IReadOnlyRepository repository,
            IDateTimeFormatter dateTimeFormatter)
        {
            _repository = repository;
            _dateTimeFormatter = dateTimeFormatter;
        }

        public async Task<PaginatedResult<ShowDto>> GetShowsAsync(GetShowsWithCastQuery query)
        {
            var totalItemsCount = await _repository.CountShowsAsync();
            var items = await _repository.GetShowsWithPagingAsync(query.PageNumber, query.PageSize);

            return new PaginatedResult<ShowDto>(ShowsToDto(items), query.PageNumber, query.PageSize, totalItemsCount);
        }

        private IEnumerable<ShowDto> ShowsToDto(IEnumerable<Show> items) =>
            items.Select(i => new ShowDto
            {
                Id = i.ExtId,
                Name = i.Name,
                Cast = i.Cast.Select(cs => cs.Character).OrderByDescending(c => c.Birthday).Select(c => new CharacterDto
                {
                    Id = c.ExtId,
                    Name = c.Name,
                    Birthday = _dateTimeFormatter.Format(c.Birthday)
                }),
            });
    }
}
