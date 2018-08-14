using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TvMazeScraper.Data.Entities
{
    [Table("Shows")]
    public class Show : Entity
    {
        public long ExtId { get; set; }

        public string Name { get; set; }

        public IEnumerable<CharacterShow> Cast { get; set; }
    }
}
