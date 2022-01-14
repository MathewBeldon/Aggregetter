namespace Aggregetter.Aggre.Application.Responses
{
    public class PagedResponse<T> : BaseResponse<T>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public int TotalRecords { get; init; }
        public bool HasNextPage { get; init; }
        public bool HasPreviousPage { get; init; }
        public PagedResponse(T data) : base(data)
        {
            if (data is null) Success = false;
        }
    }
}
