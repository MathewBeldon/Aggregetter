using Aggregetter.Aggre.Application.Exceptions;
using Aggregetter.Aggre.Application.Models.Base;
using Microsoft.AspNetCore.Http;
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

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private static Task ConvertException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var result = exception switch
            {
                ValidationException validationException => JsonSerializer.Serialize(new BaseResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = validationException.Message,
                    ValidationErrors = validationException.ValidationErrors
                }),
                RecordNotFoundException recordNotFoundException => JsonSerializer.Serialize(new BaseResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = recordNotFoundException.Message
                }),
                _ => JsonSerializer.Serialize(new BaseResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = exception.Message
                }),
            };
            return context.Response.WriteAsync(result);
        }
    }
}
