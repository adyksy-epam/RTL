using Microsoft.EntityFrameworkCore;
using RTL.TvMazeScraper.Data.Entities;

namespace RTL.TvMazeScraper.Data.Context
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext(DbContextOptions<TvMazeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>();
            modelBuilder.Entity<Character>();

            modelBuilder.Entity<CharacterShow>()
                .HasKey(cs => new {cs.CharacterId, cs.ShowId});
        }
    }
}
