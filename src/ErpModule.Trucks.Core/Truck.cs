using Ardalis.GuardClauses;
using ErpModule.Shared;
using ErpModule.Trucks.Core.Interfaces;

namespace ErpModule.Trucks.Core;

public class Truck: EntityBase, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public TruckStatus Status { get; private set; } = TruckStatus.Returning;
    public string? Description { get; private set; }

    public Truck(string code, string name, string? description = null)
    {
        Code = Guard.Against.NullOrWhiteSpace(code, nameof(code));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Description = description;
    }

    public bool ChangeStatus(TruckStatus nextStatus)
    {
        if (!Status.CanMoveTo(nextStatus)) return false;

        Status = nextStatus;
        return true;
    }

    public void ChangeCode(string newCode)
    {
        //code should be unique,
        //if changing to code which already exists there will be infrastructure error (assuming unique index in db)
        //in ddd there can be a service injected here to check uniqueness of code
        Code = Guard.Against.NullOrWhiteSpace(newCode);
    }

    public void ChangeName(string newName)
    {
        Name = Guard.Against.NullOrWhiteSpace(newName, nameof(newName));
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}
