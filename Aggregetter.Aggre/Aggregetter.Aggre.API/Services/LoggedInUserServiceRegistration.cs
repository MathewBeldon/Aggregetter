﻿using Aggregetter.Aggre.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.API.Services
{
    public sealed class LoggedInUserServiceRegistration : ILoggedInUserService
    {
        public LoggedInUserServiceRegistration(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public string UserId { get; }
    }
}