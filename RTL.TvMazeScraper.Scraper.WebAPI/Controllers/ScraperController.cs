using Microsoft.AspNetCore.Mvc;
using RTL.TvMazeScraper.Scraper.WebAPI.Commands;
using RTL.TvMazeScraper.Scraper.WebAPI.Services;

namespace RTL.TvMazeScraper.Scraper.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperController : ControllerBase
    {
        private readonly IBackgroundTaskQueue _queue;

        public ScraperController(IBackgroundTaskQueue queue)
        {
            _queue = queue;
        }

        [HttpGet]
        public IActionResult AddScrapeTask()
        {
            _queue.QueueBackgroundWorkItem(new ScrapeApiCommand());

            return Ok();
        }
    }
}