using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace RTL.TvMazeScraper.WebAPI.Results
{
    public class ServerErrorObjectResult : ObjectResult
    {
        public ServerErrorObjectResult(object value) : base(value)
        {
            this.StatusCode = (int?)HttpStatusCode.InternalServerError;
        }
    }
}
