namespace ErpModule.Trucks.Core.Interfaces;

public interface ITruckCodeUniqueValidator
{
    Task<bool> ValidateIsUnique(string truckCode);
}
