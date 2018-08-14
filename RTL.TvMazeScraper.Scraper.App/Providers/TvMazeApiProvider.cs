using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Data.Entities;
using RTL.TvMazeScraper.Scraper.App.Formatters;
using RTL.TvMazeScraper.Scraper.App.Http;
using RTL.TvMazeScraper.Scraper.App.Settings;

namespace RTL.TvMazeScraper.Scraper.App.Providers
{
    public class TvMazeApiProvider : IApiProvider
    {
        private readonly ApiSettings _settings;
        private readonly IHttpClient _client;
        private readonly IContentFormatter _formatter;

        public TvMazeApiProvider(ApiSettings settings, IHttpClient client, IContentFormatter formatter)
        {
            _settings = settings;
            _client = client;
            _formatter = formatter;
        }

        public async Task<IEnumerable<Show>> GetShowsPerPageAsync(int pageNumber)
        {
            var url = string.Format(_settings.ShowsUrlFormat, pageNumber);

            var content = await _client.GetAsync(url);
            var shows = _formatter.FormatShowContent(content);

            return shows;
        }

        public async Task<IEnumerable<Character>> GetCastPerShowIdAsync(long showId)
        {
            var url = string.Format(_settings.CastUrlFormat, showId);

            var content = await _client.GetAsync(url);
            var characters = _formatter.FormatCharacterContent(content);

            return characters.GroupBy(c => c.ExtId).Select(g => g.First());
        }
    }
}
