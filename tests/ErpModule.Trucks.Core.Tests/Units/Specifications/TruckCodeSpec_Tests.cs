using ErpModule.Trucks.Core.Specification;

namespace ErpModule.Trucks.Core.Tests.Units.Specifications;

public class TruckCodeSpec_Tests
{
    [Fact]
    public void Constructor_CreatesCodeFilter()
    {
        var spec = new TruckCodeSpecification("code");

        Assert.NotNull(spec);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_ThrowsOnInvalidCode(string code)
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            new TruckCodeSpecification(code);
        });
    }
}
