using Aggregetter.Aggre.Application.Exceptions;
using Aggregetter.Aggre.Application.Models.Base;
using FluentValidation;
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

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new BaseResponse { 
                        Success = false,
                        Message = "Validation Failed",
                        ValidationErrors = validationException.Errors.Select(error => error.ErrorMessage.ToString()).ToList()
                    });
                    break;
                case RecordNotFoundException recordNotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new BaseResponse { 
                        Success = false,
                        Message = recordNotFoundException.Message 
                    });
                    break;
                default:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new BaseResponse {
                        Success = false,
                        Message = exception.Message 
                    });
                    break;
            }
           
            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
