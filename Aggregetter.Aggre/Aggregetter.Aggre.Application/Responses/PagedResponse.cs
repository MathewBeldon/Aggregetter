namespace Aggregetter.Aggre.Application.Responses
{
    public class PagedResponse<T> : BaseResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public PagedResponse(T data) : base(data)
        {
            if (data is null) Success = false;
        }
    }
}
