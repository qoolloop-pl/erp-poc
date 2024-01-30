using Ardalis.GuardClauses;
using Ardalis.Specification;
using ErpModule.Trucks.Core.Filters;

namespace ErpModule.Trucks.Core.Specification;

public class TruckListSpecification: Specification<Truck>
{
    public TruckListSpecification(TruckListFilter filter)
    {
        filter = Guard.Against.Null(filter);

        Query
            .Where(truck => truck.Code.Contains(filter.Code!), !string.IsNullOrWhiteSpace(filter.Code))
            .Where(truck => truck.Name.Contains(filter.Name!), !string.IsNullOrWhiteSpace(filter.Name))
            .ApplyOrdering(filter);
    }
}
