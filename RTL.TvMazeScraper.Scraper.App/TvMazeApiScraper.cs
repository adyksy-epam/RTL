using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Data.Entities;
using RTL.TvMazeScraper.Data.Extensions;
using RTL.TvMazeScraper.Data.Repository;
using RTL.TvMazeScraper.Scraper.App.Providers;

namespace RTL.TvMazeScraper.Scraper.App
{
    public class TvMazeApiScraper : IApiScraper
    {
        private readonly IRepository _repository;
        private readonly IApiProvider _apiProvider;

        public TvMazeApiScraper(IApiProvider apiProvider, IRepository repository)
        {
            _apiProvider = apiProvider;
            _repository = repository;
        }

        public async Task ScrapeApiAsync(CancellationToken token)
        {
            int currentPage = 0;
            var shows = await _apiProvider.GetShowsPerPageAsync(currentPage);
            while (shows.Any())
            {
                var dbShows = await AttachShowDbInfoAsync(shows);
                foreach (var show in dbShows)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    var cast = await _apiProvider.GetCastPerShowIdAsync(show.ExtId);
                    cast = await AttachCharacterDbInfoAsync(cast);
                    
                    show.SetCast(cast);
                    await _repository.AddOrUpdateShowAsync(show);
                    await _repository.SaveChangesAsync();
                }

                ++currentPage;
                shows = await _apiProvider.GetShowsPerPageAsync(currentPage);
            }
        }

        private async Task<IEnumerable<Show>> AttachShowDbInfoAsync(IEnumerable<Show> shows)
        {
            var showIdList = shows.Select(s => s.ExtId).ToList();
            var dbShows = await _repository.GetShowsAsync(s => showIdList.Contains(s.ExtId));

            return shows.Select(s => dbShows.FirstOrDefault(db => db.ExtId == s.ExtId)?.Merge(s) ?? s);
        }

        private async Task<IEnumerable<Character>> AttachCharacterDbInfoAsync(IEnumerable<Character> cast)
        {
            var charactersIdList = cast.Select(s => s.ExtId).ToList();
            var dbCharacters = await _repository.GetCharactersAsync(s => charactersIdList.Contains(s.ExtId));

            return cast.Select(s => dbCharacters.FirstOrDefault(db => db.ExtId == s.ExtId)?.Merge(s) ?? s);
        }
    }
}
