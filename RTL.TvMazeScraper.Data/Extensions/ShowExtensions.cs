using System.Collections.Generic;
using System.Linq;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Data.Extensions
{
    public static class ShowExtensions
    {
        public static Show SetCast(this Show show, IEnumerable<Character> cast)
        {
            show.Cast = cast.Select(c => new CharacterShow
            {
                Show = show,
                Character = c,
            })
            .ToList();

            return show;
        }

        public static Show Merge(this Show show, Show fromItem)
        {
            show.Name = fromItem.Name;

            return show;
        }
    }
}
