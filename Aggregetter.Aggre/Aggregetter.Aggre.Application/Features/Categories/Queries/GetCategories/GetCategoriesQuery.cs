﻿using Aggregetter.Aggre.Application.Pipelines.Caching;
using Mediator;
using System;

namespace Aggregetter.Aggre.Application.Features.Categories.Queries.GetCategories
{
    public sealed class GetCategoriesQuery : IRequest<GetCategoriesQueryResponse>, ICacheableRequest
    {
        public string Key => $"{nameof(GetCategoriesQuery)}";
        public bool Bypass { get; init; }
        public TimeSpan? AbsoluteExpiration { get; init; }
    }
}
