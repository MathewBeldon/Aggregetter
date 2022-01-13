using Aggregetter.Aggre.Application.Requests;
using System;

namespace Aggregetter.Aggre.Application.Services.PaginationService
{
    public sealed class PaginationService : IPaginationService
    {
        public (bool PreviousPage, bool NextPage)  GetPagedUris(PagedRequest pageRequest, string endpoint, int recordCount)
        {
            bool previousPage = false;
            if (pageRequest.Page > 1)
            {
                previousPage = true;
            }

            var lastPageNumber = Math.Ceiling((double)recordCount / pageRequest.PageSize);
            bool nextPage = false;
            if (pageRequest.Page + 1 <= lastPageNumber)
            {
                nextPage = true;
            }

            return (previousPage, nextPage);
        }
    }
}
