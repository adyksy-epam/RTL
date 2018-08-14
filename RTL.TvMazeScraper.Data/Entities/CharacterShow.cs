namespace RTL.TvMazeScraper.Data.Entities
{
    public class CharacterShow
    {
        public long ShowId { get; set; }
        public Show Show { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
