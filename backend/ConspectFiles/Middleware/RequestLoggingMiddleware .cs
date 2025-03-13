using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ConspectFiles.Middleware
{
    public class RequestLoggingMiddleware 
    {
        private readonly RequestDelegate _next;
        ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware (RequestDelegate next, ILogger<RequestLoggingMiddleware> logger )
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
            await _next(context);
            _logger.LogInformation($"Status: {context.Response.StatusCode}");
        }
    }
}