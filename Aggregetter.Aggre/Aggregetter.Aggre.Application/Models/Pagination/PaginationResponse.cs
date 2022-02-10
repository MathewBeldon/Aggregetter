using Aggregetter.Aggre.Application.Models.Base;
using Aggregetter.Aggre.Application.Pipelines.Pagination;
using System;

namespace Aggregetter.Aggre.Application.Models.Pagination
{
    public class PaginationResponse<T> : ContentResponse<T>, IPaginationResponse
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int RecordCount { get; private set; }

        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public PaginationResponse() : base() { }
        public PaginationResponse(T data, int page, int pageSize, int recordCount) : base(data)
        {
            if (data is null) Success = false;

            Page = page >= 0 ? page : throw new ArgumentOutOfRangeException(nameof(page));
            PageSize = pageSize >= 0 ? pageSize : throw new ArgumentOutOfRangeException(nameof(pageSize));
            RecordCount = recordCount >= 0 ? recordCount : throw new ArgumentOutOfRangeException(nameof(recordCount));
        }
    }
}
