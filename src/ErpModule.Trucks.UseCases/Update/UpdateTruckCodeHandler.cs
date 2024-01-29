using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.Core.Services;

namespace ErpModule.Trucks.UseCases.Update;

public class UpdateTruckCodeHandler : ICommandHandler<UpdateTruckCodeCommand, Result>
{
    private readonly IRepository<Truck> _repository;

    public UpdateTruckCodeHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateTruckCodeCommand request, CancellationToken cancellationToken)
    {
        var existingTruck = await _repository.GetByIdAsync(request.TruckId, cancellationToken);
        if (existingTruck is null) return Result.NotFound();

        try
        {
            existingTruck.ChangeCode(request.Code);
            await _repository.UpdateAsync(existingTruck, cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }
    }
}
