using System;

namespace RTL.TvMazeScraper.Domain.Queries
{
    public class GetShowsWithCastQuery
    {
        public GetShowsWithCastQuery(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException(nameof(pageNumber));
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException(nameof(pageSize));
            }

            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageSize { get; }

        public int PageNumber { get; }
    }
}
