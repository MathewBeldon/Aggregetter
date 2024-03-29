﻿using Aggregetter.Aggre.Application.Pipelines.Pagination;

namespace Aggregetter.Aggre.Application.Models.Base
{
    public class PaginationResponse<T> : ContentResponse<T>, IPaginationResponse
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int RecordCount { get; init; }

        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public PaginationResponse() : base() { }
        public PaginationResponse(T data) : base(data) { }
    }
}
