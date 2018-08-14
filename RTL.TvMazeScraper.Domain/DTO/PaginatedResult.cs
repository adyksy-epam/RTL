using System;
using System.Collections.Generic;
using System.Linq;

namespace RTL.TvMazeScraper.Domain.DTO
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(IEnumerable<T> items, int pageNumber, int pageSize, long totalItemsCount)
        {
            Items = items ?? Enumerable.Empty<T>();
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItemsCount = totalItemsCount;
            TotalPagesCount = CalculateTotalPagesCount();
        }

        public IEnumerable<T> Items { get; }

        public int PageSize { get; }

        public int PageNumber { get; }

        public long TotalItemsCount { get; }

        public long TotalPagesCount { get; }

        private long CalculateTotalPagesCount()
        {
            if (PageSize <= 0)
            {
                return 0;
            }
            return (long)Math.Ceiling(TotalItemsCount / (decimal)PageSize);
        }
    }
}
