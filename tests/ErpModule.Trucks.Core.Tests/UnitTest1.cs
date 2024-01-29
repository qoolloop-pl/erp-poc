using ErpModule.Shared.Specification;
using ErpModule.Shared.Specification.List;
using ErpModule.Trucks.Core.Filters;
using ErpModule.Trucks.Core.Specification;

namespace ErpModule.Trucks.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var catalog = new List<Truck>
        {
            new Truck("Code1", "Name 1", "Test1"),
            new Truck("Code2", "Name 2", "Test2"),
            new Truck("Code3", "Name 3", "Test3"),
            new Truck("Code4", "Name 4", "Test4"),
        };

        var spec = new TruckListSpecification(new TruckListFilter
        {
            Code = "Code1",
            Name = "Name 1"
        });

        var trucks = spec.Evaluate(catalog).ToList();

        Assert.Single(trucks);
    }
}
