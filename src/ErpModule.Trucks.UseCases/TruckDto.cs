namespace ErpModule.Trucks.UseCases;

public record TruckDto(Guid Id, string Code, string Name, string? Description, string Status);
