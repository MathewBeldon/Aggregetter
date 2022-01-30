namespace Aggregetter.Aggre.Application.Pipelines.Pagination
{
    public interface IPaginationRequest
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}
