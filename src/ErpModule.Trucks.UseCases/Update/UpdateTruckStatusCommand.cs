using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;

namespace ErpModule.Trucks.UseCases.Update;

public record UpdateTruckStatusCommand(Guid TruckId, TruckStatus NexStatus): ICommand<Result>;
