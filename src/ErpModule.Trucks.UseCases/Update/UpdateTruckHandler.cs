using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;

namespace ErpModule.Trucks.UseCases.Update;

public class UpdateTruckHandler : ICommandHandler<UpdateTruckCommand, Result<TruckDto>>
{
    private readonly IRepository<Truck> _repository;

    public UpdateTruckHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result<TruckDto>> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
    {
        var existingTruck = await _repository.GetByIdAsync(request.TruckId, cancellationToken);
        if (existingTruck is null) return Result.NotFound();

        existingTruck.ChangeName(request.Name);
        existingTruck.UpdateDescription(request.Description);

        await _repository.UpdateAsync(existingTruck, cancellationToken);

        return Result.Success(
            new TruckDto(
                existingTruck.Id,
                existingTruck.Code,
                existingTruck.Name,
                existingTruck.Description,
                existingTruck.Status.Name));
    }
}