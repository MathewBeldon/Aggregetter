using Aggregetter.Aggre.Application.Models.Pagination;

namespace Aggregetter.Aggre.Application.Models.Base
{
    public class PaginationRequest : IPaginationRequest
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
