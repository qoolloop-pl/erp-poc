using Ardalis.GuardClauses;
using Ardalis.Specification;

namespace ErpModule.Trucks.Core.Specification;

public class TruckCodeSpecification: Specification<Truck>
{
    public TruckCodeSpecification(string truckCode)
    {
        truckCode = Guard.Against.NullOrWhiteSpace(truckCode);

        Query.Where(truck => truck.Code.Equals(truckCode));
    }
}
