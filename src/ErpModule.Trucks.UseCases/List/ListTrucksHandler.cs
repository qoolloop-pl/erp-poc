using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Shared.Specification;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.Core.Specification;

namespace ErpModule.Trucks.UseCases.List;

public class ListTrucksHandler: IQueryHandler<ListTrucksQuery, Result<PagedList<TruckDto>>>
{
    private readonly IRepository<Truck> _repository;

    public ListTrucksHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedList<TruckDto>>> Handle(ListTrucksQuery request, CancellationToken cancellationToken)
    {
        var specification = new TruckListSpecification(request.TruckFilter);

        var trucks = await _repository.ListPagedAsync(specification, request.TruckFilter, cancellationToken);

        var dtos = trucks.Data.Select(truck =>
            new TruckDto(
                truck.Id,
                truck.Code,
                truck.Name,
                truck.Description,
                truck.Status.Name
            )).ToList();

        return Result.Success(new PagedList<TruckDto>(dtos, trucks.Page));
    }
}
