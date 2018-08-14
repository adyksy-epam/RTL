using System;

namespace RTL.TvMazeScraper.Scraper.App.Converters
{
    public interface IDateTimeConverter
    {
        DateTime? Convert(string existingValue);
    }
}