using System.Collections.Generic;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Scraper.App.Formatters
{
    public interface IContentFormatter
    {
        IEnumerable<Show> FormatShowContent(string content);
        IEnumerable<Character> FormatCharacterContent(string content);
    }
}
