using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;

namespace ErpModule.Trucks.UseCases.Create;

public class CreateTruckHandler: ICommandHandler<CreateTruckCommand, Result<Guid>>
{
    private readonly IRepository<Truck> _repository;

    public CreateTruckHandler(IRepository<Truck> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = new Truck(request.Code, request.Name);
        truck.UpdateDescription(request.Description);

        try
        {
            var createdTruck = await _repository.AddAsync(truck, cancellationToken);
            return createdTruck.Id;
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }
    }
}
