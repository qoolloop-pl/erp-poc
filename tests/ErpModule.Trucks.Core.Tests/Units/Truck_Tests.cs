using System.Net.Sockets;

namespace ErpModule.Trucks.Core.Tests.Units;

public class Truck_Tests
{
    private readonly string _testCode = "code";
    private readonly string _testName = "name";
    private readonly string _testDescription = "description";

    private Truck GetTestTruck()
    {
        return new Truck(_testCode, _testName, _testDescription);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("\t", "")]
    public void Constructor_ThrowsOnImproperCode(string code, string name)
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            new Truck(code, name);
        });
    }

    [Fact]
    public void Constructor_InitializesFields()
    {
        var truck = new Truck(_testCode, _testName, _testDescription);

        Assert.Equal(_testCode, truck.Code);
        Assert.Equal(_testName, truck.Name);
        Assert.Equal(_testDescription, truck.Description);
    }

    [Fact]
    public void Constructor_InitializesStatusToReturning()
    {
        var truck = GetTestTruck();

        Assert.Equal(TruckStatus.Returning, truck.Status);
    }

    [Fact]
    public void ChangeStatus_ReturnsTrueForValidTransition()
    {
        var truck = GetTestTruck();
        var result = truck.ChangeStatus(TruckStatus.OutOfService);

        Assert.True(result);
        Assert.Equal(TruckStatus.OutOfService, truck.Status);
    }

    [Fact]
    public void ChangeStatus_ReturnsFalseForInvalidTransition()
    {
        var truck = GetTestTruck();
        var result = truck.ChangeStatus(TruckStatus.AtJob);

        Assert.False(result);
        Assert.Equal(TruckStatus.Returning, truck.Status);
    }

    [Fact]
    public void ChangeCode_UpdatesCodeIfValid()
    {
        var newCode = "new code";

        var truck = GetTestTruck();
        truck.ChangeCode(newCode);

        Assert.Equal(newCode, truck.Code);
    }

    [Fact]
    public void ChangeCode_ThrowsIfInvalid()
    {
        var truck = GetTestTruck();

        Assert.ThrowsAny<Exception>(() =>
        {
            truck.ChangeCode(null!);
        });
    }

    [Fact]
    public void ChangeName_UpdatesNameIfValid()
    {
        var newName = "new name";

        var truck = GetTestTruck();
        truck.ChangeName(newName);

        Assert.Equal(newName, truck.Name);
    }

    [Fact]
    public void ChangeName_ThrowsIfInvalid()
    {
        var truck = GetTestTruck();

        Assert.ThrowsAny<Exception>(() =>
        {
            truck.ChangeName(null!);
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("new description")]
    public void ChangeDescription_UpdatesDescriptionField(string? description)
    {
        var truck = GetTestTruck();
        truck.UpdateDescription(description);

        Assert.Equal(description, truck.Description);
    }
}
