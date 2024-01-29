using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.Core.Specification;

namespace ErpModule.Trucks.UseCases.List;

public class ListTrucksHandler: IQueryHandler<ListTrucksQuery, Result<IEnumerable<TruckDto>>>
{
    private readonly IRepository<Truck> _repository;

    public ListTrucksHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<TruckDto>>> Handle(ListTrucksQuery request, CancellationToken cancellationToken)
    {
        var specification = new TruckListSpecification(request.TruckFilter);

        var trucks = await _repository.ListAsync(specification, cancellationToken);

        var dtos = trucks.Select(truck =>
            new TruckDto(
                truck.Id,
                truck.Code,
                truck.Name,
                truck.Description,
                truck.Status.Name
            ));

        return Result.Success(dtos);
    }
}
