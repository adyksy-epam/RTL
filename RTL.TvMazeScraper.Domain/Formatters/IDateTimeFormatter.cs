using System;

namespace RTL.TvMazeScraper.Domain.Formatters
{
    public interface IDateTimeFormatter
    {
        string Format(DateTime? dt);
    }
}
