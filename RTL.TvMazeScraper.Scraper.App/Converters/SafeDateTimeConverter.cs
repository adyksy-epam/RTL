using System;

namespace RTL.TvMazeScraper.Scraper.App.Converters
{
    public class SafeDateTimeConverter : IDateTimeConverter
    {
        public DateTime? Convert(string existingValue)
        {
            if (string.IsNullOrWhiteSpace(existingValue))
            {
                return null;
            }

            if (DateTime.TryParse(existingValue, out DateTime result))
            {
                return result;
            }

            return null;
        }
    }
}
