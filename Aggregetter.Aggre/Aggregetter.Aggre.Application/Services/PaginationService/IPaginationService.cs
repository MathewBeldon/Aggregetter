namespace Aggregetter.Aggre.Application.Services.PaginationService
{
    public interface IPaginationService
    {
        (bool PreviousPage, bool NextPage) GetPagedUris(int pageSize, int page, int recordCount);
    }
}
