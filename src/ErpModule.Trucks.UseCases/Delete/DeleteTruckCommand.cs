using Ardalis.Result;
using ErpModule.Shared;

namespace ErpModule.Trucks.UseCases.Delete;

public record DeleteTruckCommand(Guid TruckId): ICommand<Result>;
