using Ardalis.Result;
using ErpModule.Shared;

namespace ErpModule.Trucks.UseCases.Update;

public record UpdateTruckCommand(Guid TruckId, string Name, string? Description): ICommand<Result<TruckDto>>;
