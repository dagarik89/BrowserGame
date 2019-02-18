using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BrowserGame.Filters
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger) //<ExceptionHandlerMiddleware>
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exc)
            {
                /*try
                {
                    _logger.LogError(0, exception, exception.Message);
                    //Обработка ошибки, к примеру, просто можем логировать сообщение ошибки
                }
                catch (Exception innerException)
                {
                    _logger.LogError(0, innerException, "Ошибка обработки исключения");
                }
                // Если в коде обработки ошибки мы снова получили ошибку, то пробрасываем ее выше по цепочке Middleware
                throw;*/
                //_logger.LogError(context, exc);
                _logger.LogError(0, exc, exc.Message);
                await HandleExceptionAsync(context, exc.GetBaseException());
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exp)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = JsonConvert.SerializeObject(new { Code = code, Message = exp.Message, StackTrace = exp.StackTrace });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
