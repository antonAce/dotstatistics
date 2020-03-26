using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Regression.API.Filters
{
    public class DatasetExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception.GetType() == typeof(ArgumentException) ? 
                new ContentResult { StatusCode = 400, Content = "Input arguments are invalid!" } : 
                new ContentResult { StatusCode = 500, Content = "Oops! Something went wrong!" };

            context.ExceptionHandled = true;
        }
    }
}