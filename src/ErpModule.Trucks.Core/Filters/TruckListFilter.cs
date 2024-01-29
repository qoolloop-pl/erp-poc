using ErpModule.Shared.Specification.List;

namespace ErpModule.Trucks.Core.Filters;

public class TruckListFilter: ListFilterBase
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}
