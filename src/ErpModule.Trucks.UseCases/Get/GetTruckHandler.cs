using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;

namespace ErpModule.Trucks.UseCases.Get;

public class GetTruckHandler : ICommandHandler<GetTruckQuery, Result<TruckDto>>
{
    private readonly IRepository<Truck> _repository;

    public GetTruckHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result<TruckDto>> Handle(GetTruckQuery request, CancellationToken cancellationToken)
    {
        var truck = await _repository.GetByIdAsync(request.TruckId, cancellationToken);

        if (truck is null) return Result.NotFound();

        return new TruckDto(
            truck.Id,
            truck.Code,
            truck.Name,
            truck.Description,
            truck.Status.Name
        );
    }
}
