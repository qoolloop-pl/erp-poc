using Ardalis.Result;
using ErpModule.Shared;

namespace ErpModule.Trucks.UseCases.Get;

public record GetTruckQuery(Guid TruckId): ICommand<Result<TruckDto>>;
