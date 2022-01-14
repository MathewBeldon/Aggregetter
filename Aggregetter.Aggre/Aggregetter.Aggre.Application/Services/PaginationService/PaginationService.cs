﻿using Aggregetter.Aggre.Application.Requests;
using System;

namespace Aggregetter.Aggre.Application.Services.PaginationService
{
    public sealed class PaginationService : IPaginationService
    {
        public (bool PreviousPage, bool NextPage)  GetPagedUris(PagedRequest pageRequest, string endpoint, int recordCount)
        {
            bool previousPage = pageRequest.Page > 1;

            var lastPageNumber = Math.Ceiling((double)recordCount / pageRequest.PageSize);
            bool nextPage = pageRequest.Page + 1 <= lastPageNumber;

            return (previousPage, nextPage);
        }
    }
}