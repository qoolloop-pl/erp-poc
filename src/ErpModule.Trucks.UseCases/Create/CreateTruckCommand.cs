using Ardalis.Result;
using ErpModule.Shared;

namespace ErpModule.Trucks.UseCases.Create;

public record CreateTruckCommand(string Code, string Name, string? Description) : ICommand<Result<Guid>>;
