using System;

namespace Aggregetter.Aggre.Application.Services.PaginationService
{
    public sealed class PaginationService : IPaginationService
    {
        public (bool PreviousPage, bool NextPage)  GetPagedUris(int pageSize, int page, int recordCount)
        {
            bool previousPage = page > 1;

            var lastPageNumber = Math.Ceiling((double)recordCount / pageSize);
            bool nextPage = page + 1 <= lastPageNumber;

            return (previousPage, nextPage);
        }
    }
}
