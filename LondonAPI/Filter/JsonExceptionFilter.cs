using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace LondonAPI.Filter
{
    public class JsonExceptionFilter :IExceptionFilter
    {
        private readonly IHostEnvironment _env;

        public JsonExceptionFilter(IHostEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();
            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A Server Error Occured";
                error.Detail = context.Exception.Message;
            }
            context.Result = new ObjectResult(error)
            {
                StatusCode = 500    
            };
        }
    }
}
