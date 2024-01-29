using Ardalis.GuardClauses;
using Ardalis.Result;
using ErpModule.Shared;

namespace ErpModule.Trucks.UseCases.Update;

public record UpdateTruckCodeCommand(Guid TruckId, string Code): ICommand<Result>;
