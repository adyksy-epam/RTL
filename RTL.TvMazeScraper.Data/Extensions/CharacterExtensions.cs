using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Data.Extensions
{
    public static class CharacterExtensions
    {
        public static Character Merge(this Character character, Character fromItem)
        {
            character.Name = fromItem.Name;
            character.Birthday = fromItem.Birthday;

            return character;
        }
    }
}
