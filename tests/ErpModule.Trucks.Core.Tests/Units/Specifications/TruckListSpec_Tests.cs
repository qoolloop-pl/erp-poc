using ErpModule.Trucks.Core.Filters;
using ErpModule.Trucks.Core.Specification;

namespace ErpModule.Trucks.Core.Tests.Units.Specifications;

public class TruckListSpec_Tests
{
    [Fact]
    public void Constructor_CreatesWithEmptyFilter()
    {
        var spec = new TruckListSpecification(new TruckListFilter());

        Assert.NotNull(spec);
    }

    [Fact]
    public void Constructor_ThrowsWithNullFilter()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new TruckListSpecification(null);
        });
    }
}
