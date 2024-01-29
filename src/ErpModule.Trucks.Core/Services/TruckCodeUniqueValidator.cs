using ErpModule.Shared;
using ErpModule.Trucks.Core.Interfaces;
using ErpModule.Trucks.Core.Specification;

namespace ErpModule.Trucks.Core.Services;

public class TruckCodeUniqueValidator: ITruckCodeUniqueValidator
{
    private readonly IRepository<Truck> _repository;

    public TruckCodeUniqueValidator(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<bool> ValidateIsUnique(string truckCode)
    {
        return !await _repository.AnyAsync(new TruckCodeSpecification(truckCode));
    }
}
