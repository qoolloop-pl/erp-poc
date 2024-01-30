using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;

namespace ErpModule.Trucks.UseCases.Update;

public class UpdateTruckStatusHandler : ICommandHandler<UpdateTruckStatusCommand, Result>
{
    private readonly IRepository<Truck> _repository;

    public UpdateTruckStatusHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateTruckStatusCommand request, CancellationToken cancellationToken)
    {
        var existingTruck = await _repository.GetByIdAsync(request.TruckId, cancellationToken);
        if (existingTruck is null) return Result.NotFound();

        if (!existingTruck.ChangeStatus(request.NexStatus)) return Result.Forbidden();

        await _repository.UpdateAsync(existingTruck, cancellationToken);
        return Result.Success();
    }
}
