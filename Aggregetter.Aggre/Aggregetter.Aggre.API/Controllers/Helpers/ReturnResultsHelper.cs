using Aggregetter.Aggre.Application.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Aggregetter.Aggre.API.Controllers.Helpers
{
    public static class ReturnResultsHelper
    {
        public static IActionResult ReturnResult(this BaseResponse baseResponse)
        {
            return baseResponse.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(baseResponse),
                HttpStatusCode.NotFound => new NotFoundObjectResult(baseResponse),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(baseResponse),
                _ => throw new System.Exception()
            };            
        }
    }
}
