using System.Collections.Generic;

namespace RTL.TvMazeScraper.Domain.DTO
{
    public class ShowDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CharacterDto> Cast { get; set; }
    }
}
