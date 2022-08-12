namespace Aggregetter.Aggre.Application.Models.Pagination
{ 
    public interface IPaginationRequest
    {
        public int PageSize { get; set; } 
        public int Page { get; set; }
    }
}
