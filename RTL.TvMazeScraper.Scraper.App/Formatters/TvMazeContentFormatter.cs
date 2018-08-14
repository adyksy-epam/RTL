using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTL.TvMazeScraper.Data.Entities;
using RTL.TvMazeScraper.Scraper.App.Converters;
using RTL.TvMazeScraper.Scraper.App.DTO;

namespace RTL.TvMazeScraper.Scraper.App.Formatters
{
    public class TvMazeContentFormatter : IContentFormatter
    {
        private readonly IDateTimeConverter _dateConverter;

        public TvMazeContentFormatter(IDateTimeConverter dateConverter)
        {
            _dateConverter = dateConverter;
        }

        public IEnumerable<Show> FormatShowContent(string content)
        {
            var showDTOs = JsonConvert.DeserializeObject<IEnumerable<TvMazeShowDTO>>(content) ?? Enumerable.Empty<TvMazeShowDTO>();

            return FromDTO(showDTOs);
        }

        public IEnumerable<Character> FormatCharacterContent(string content)
        {
            var persons = JArray.Parse(content).SelectTokens("$..person");
            var personDTOs = persons.Select(p => JsonConvert.DeserializeObject<TvMazePersonDTO>(p.ToString())).ToList();

            return FromDTO(personDTOs);
        }

        private IEnumerable<Show> FromDTO(IEnumerable<TvMazeShowDTO> dtos)
        {
            return dtos.Select(dto => new Show
            {
                ExtId = dto.Id,
                Name = dto.Name
            })
            .ToList();
        }

        private IEnumerable<Character> FromDTO(IEnumerable<TvMazePersonDTO> dtos)
        {
            return dtos.Select(dto => new Character
            {
                ExtId = dto.Id,
                Name = dto.Name,
                Birthday = _dateConverter.Convert(dto.Birthday)
            })
            .ToList();
        }
    }
}
