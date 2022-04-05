namespace Application.Filters
{
    public class PaginationInfoFilter
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
