using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;

namespace ErpModule.Trucks.UseCases.Delete;

public class DeleteTruckHandler: ICommandHandler<DeleteTruckCommand, Result>
{
    private readonly IRepository<Truck> _repository;

    public DeleteTruckHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var truckToDelete = await _repository.GetByIdAsync(request.TruckId, cancellationToken);
        if (truckToDelete is null)
        {
            return Result.NotFound();
        }

        await _repository.DeleteAsync(truckToDelete, cancellationToken);

        return Result.Success();
    }
}
