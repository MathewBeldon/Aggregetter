namespace Aggregetter.Aggre.Application.Pipelines.Pagination
{
    public interface IPaginationResponse
    {
        public int Page { get; }
        public int PageSize { get; }
        public int RecordCount { get; }

        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}
