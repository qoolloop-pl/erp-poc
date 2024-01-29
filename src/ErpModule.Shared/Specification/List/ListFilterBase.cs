namespace ErpModule.Shared.Specification.List;

public class ListFilterBase
{
    public int? Skip { get; set; }
    public int? Take { get; set; }
    /// <summary>
    /// Name of property
    /// </summary>
    public string? SortBy { get; set; }
    /// <summary>
    /// order direction
    /// </summary>
    public string? OrderBy { get; set; }
}
