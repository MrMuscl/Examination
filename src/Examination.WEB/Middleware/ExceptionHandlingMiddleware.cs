using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Examination.WEB.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) 
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex)
            {
                await HandleExceptionAsync(context, ex.Message, ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, ex.InnerException?.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, string innerMessasge) 
        {
            _logger.LogError(message);
            HttpResponse response = context.Response;
            string str = "";

            if (string.IsNullOrEmpty(innerMessasge))
            {
                str = message;
            }
            else 
            {
                str = $"{message}" + Environment.NewLine +
                         $"Inner exception: {innerMessasge}";
            }
            
            await response.WriteAsync(str);
        }
    }
}
