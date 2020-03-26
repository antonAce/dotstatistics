using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Regression.API.Filters
{
    public class ProcessingExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly IDictionary<Type, string> _exceptionResponses = new Dictionary<Type, string>
        {
            {typeof(ArgumentException), "Input arguments are invalid!"},
            {typeof(ArgumentNullException), "Input arguments cannot be empty!"},
            {typeof(DivideByZeroException), "Input arguments caused zero division exception!"},
            {typeof(InvalidOperationException), "This operation is not allowed!"}
        };
        
        public void OnException(ExceptionContext context)
        {
            foreach (var pair in _exceptionResponses)
            {
                if (context.Exception.GetType() == pair.Key)
                {
                    context.Result = new ContentResult { StatusCode = 400, Content = pair.Value };
                    context.ExceptionHandled = true;
                    return;
                }
            }
            
            context.Result = new ContentResult { StatusCode = 500, Content = "Oops! Something went wrong!" };
            context.ExceptionHandled = true;
        }
    }
}