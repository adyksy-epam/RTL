using System;

namespace RTL.TvMazeScraper.Domain.Formatters
{
    public class BirthDayFormatter : IDateTimeFormatter
    {
        public string Format(DateTime? dt)
        {
            if (!dt.HasValue)
            {
                return string.Empty;
            }

            return dt.Value.ToShortDateString();
        }
    }
}
