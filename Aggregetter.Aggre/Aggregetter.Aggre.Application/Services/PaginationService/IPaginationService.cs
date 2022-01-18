using Aggregetter.Aggre.Application.Models.Pagination;

namespace Aggregetter.Aggre.Application.Services.PaginationService
{
    public interface IPaginationService
    {
        (bool PreviousPage, bool NextPage) GetPagedUris(PaginationRequest pageRequest, string endpoint, int recordCount);
    }
}
