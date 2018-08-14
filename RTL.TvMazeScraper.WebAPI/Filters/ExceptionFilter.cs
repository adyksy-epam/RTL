using Microsoft.AspNetCore.Mvc.Filters;
using RTL.TvMazeScraper.WebAPI.Results;

namespace RTL.TvMazeScraper.WebAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ServerErrorObjectResult(context.Exception.Message);
        }
    }
}
