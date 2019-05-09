using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IPORevamp.WebAPI.Models;

namespace IPORevamp.WebAPI.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                if (context.Exception is ApplicationException)
                {
                    context.Result = new BadRequestObjectResult(new ApiResponse(context.Exception.Message, HttpStatusCode.BadGateway, null, true));
                }
                else
                {
                    context.Result = new BadRequestObjectResult(new ApiResponse("Critical error occurred!, try again later"));
                }
                _logger.LogError(new EventId(), context.Exception, context.Exception.Message);
                context.ExceptionHandled = true;
            }
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                if (context.Exception is ApplicationException)
                {
                    context.Result = new BadRequestObjectResult(new ApiResponse(context.Exception.Message, HttpStatusCode.BadGateway, null, true));
                }
                else
                {
                    context.Result = new BadRequestObjectResult(new ApiResponse($"{context.Exception.GetType()} error has occurred!, try again later"));
                }

                _logger.LogError(new EventId(), context.Exception, context.Exception.Message);
                context.ExceptionHandled = true;
            }
            return base.OnExceptionAsync(context);
        }
    }
}
