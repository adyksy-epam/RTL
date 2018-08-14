using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTL.TvMazeScraper.Domain.Providers;
using RTL.TvMazeScraper.Domain.Queries;

namespace RTL.TvMazeScraper.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowProvider _showProvider;

        public ShowsController(IShowProvider showProvider)
        {
            _showProvider = showProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetShowsAsync(int pageNumber = 1, int pageSize = 100)
        {
            GetShowsWithCastQuery query;

            try
            {
                query = new GetShowsWithCastQuery(pageNumber, pageSize);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            var result = await _showProvider.GetShowsAsync(query);

            return Ok(result);
        }
    }
}