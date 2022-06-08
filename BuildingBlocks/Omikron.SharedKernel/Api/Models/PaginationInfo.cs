namespace Omikron.SharedKernel.Api.Models
{
    public class PaginationInfo
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }
    }
}