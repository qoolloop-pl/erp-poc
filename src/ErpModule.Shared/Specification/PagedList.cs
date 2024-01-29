using ErpModule.Shared.Specification.List;

namespace ErpModule.Shared.Specification;

public class PagedList<T>
{
    public List<T> Data { get; private set; }
    public Pagination Page { get; private set; }

    public PagedList(List<T> data, Pagination page)
    {
        Data = data;
        Page = page;
    }
}

public class Pagination
{
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }

    public Pagination(ListFilterBase filter, int totalItems)
    {
        TotalItems = totalItems;
        PageSize = filter.PageSize ?? 25;
        TotalPages = TotalItems == 0 ? 1 : (int)Math.Ceiling((decimal)TotalItems / PageSize);
        Page = filter.Page is null or <= 0 ? 1 : filter.Page.Value;

        Take = PageSize;
        Skip = PageSize * (Page - 1);
    }
}
