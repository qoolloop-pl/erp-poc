using Ardalis.Specification;
using ErpModule.Trucks.Core.Filters;

namespace ErpModule.Trucks.Core.Specification;

public class TruckListSpecification: Specification<Truck>
{
    public TruckListSpecification(TruckListFilter filter)
    {
        Query
            .Where(truck => truck.Code.Contains(filter.Code!), !string.IsNullOrWhiteSpace(filter.Code))
            .Where(truck => truck.Name.Contains(filter.Name!), !string.IsNullOrWhiteSpace(filter.Name))
            .ApplyOrdering(filter)
            .Skip(filter.Skip ?? 0)
            .Take(filter.Take ?? 25);
    }
}
