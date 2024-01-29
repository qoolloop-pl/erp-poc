using Ardalis.Specification;
using ErpModule.Shared.Specification.List;

namespace ErpModule.Trucks.Core.Specification;

public static class TruckListSpecificationExtensions
{
    public static ISpecificationBuilder<Truck> ApplyOrdering(this ISpecificationBuilder<Truck> builder, ListFilterBase? filter = null)
    {
        // If there is no filter apply default ordering;
        if (filter is null) return builder.OrderBy(x => x.Id);

        // We want the "asc" to be the default, that's why the condition is reverted.
        var isAscending = !(filter.OrderBy?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false);

        return filter.SortBy switch
        {
            nameof(Truck.Code) => isAscending ? builder.OrderBy(x => x.Code) : builder.OrderByDescending(x => x.Code),
            nameof(Truck.Name) => isAscending ? builder.OrderBy(x => x.Name) : builder.OrderByDescending(x => x.Name),
            _ => builder.OrderBy(x => x.Id)
        };
    }
}
