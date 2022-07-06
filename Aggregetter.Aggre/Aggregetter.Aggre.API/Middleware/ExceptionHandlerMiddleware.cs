using Aggregetter.Aggre.Application.Exceptions;
using Aggregetter.Aggre.Application.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex, _logger);
            }
        }

        private static Task ConvertException(HttpContext context, Exception exception, ILogger logger)
        {
            context.Response.ContentType = "application/json";

            string result;
            HttpStatusCode httpStatusCode;
            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new BaseResponse
                    {
                        Message = validationException.Message,
                        ValidationErrors = validationException.ValidationErrors
                    });
                    break;
                case RecordNotFoundException recordNotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new BaseResponse
                    {
                        Message = recordNotFoundException.Message
                    });
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new BaseResponse
                    {
                        Message = ""
                    });
                    logger.LogError(exception, "Unhandled error from middleware");
                    break;
            }
            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
