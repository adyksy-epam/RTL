using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TvMazeScraper.Data.Entities
{
    [Table("Characters")]
    public class Character : Entity
    {
        public long ExtId { get; set; }

        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        public IEnumerable<CharacterShow> Shows { get; set; }
    }
}
