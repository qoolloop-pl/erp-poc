using System.Reflection;
using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.UseCases.Get;
using ErpModule.Trucks.UseCases.Update;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace ErpModule.Trucks.UseCases.Tests.Units.Update;

public class UpdateTruckHandler_Tests
{
    private readonly Guid _testTruckId = new Guid("9CC867D3-3A7D-4794-BB94-1E4F4EB3BA1D");

    private Truck CreateTruck()
    {
        return new Truck("code", "name", "description")
        {
            Id = _testTruckId
        };
    }

    [Fact]
    public async Task Handle_ReturnsSuccessOnValidCommand()
    {
        var newName = "new name";
        var newDescription = "new description";

        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(CreateTruck());

        var sut = new UpdateTruckHandler(repository);

        var result = await sut.Handle(new UpdateTruckCommand(_testTruckId, newName, newDescription), CancellationToken.None);

        Assert.True(result.IsSuccess);
        await repository.Received().UpdateAsync(Arg.Is<Truck>(t => t.Name.Equals(newName)), Arg.Any<CancellationToken>());
        await repository.Received().UpdateAsync(Arg.Is<Truck>(t => t.Description.Equals(newDescription)), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ReturnsNotFoundOnNoTruckInRepo()
    {
        var newName = "new name";
        var newDescription = "new description";

        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns((Truck?)null);

        var sut = new UpdateTruckHandler(repository);

        var result = await sut.Handle(new UpdateTruckCommand(_testTruckId, newName, newDescription), Arg.Any<CancellationToken>());

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
