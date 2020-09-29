using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Regression.API.Filters
{
    public sealed class GlobalExceptionFilter : IExceptionFilter
    {
        private ILogger Logger { get; }
        
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            Logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "A server error has occurred!"
            };
            context.ExceptionHandled = true;
        }
    }
}