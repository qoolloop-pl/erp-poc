using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.UseCases.Delete;
using NSubstitute;

namespace ErpModule.Trucks.UseCases.Tests.Units.Delete;

public class DeleteTruckHandler_Tests
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
        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(CreateTruck());

        var sut = new DeleteTruckHandler(repository);

        var result = await sut.Handle(new DeleteTruckCommand(_testTruckId), CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ReturnsNotFoundOnNoTruckInRepo()
    {
        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns((Truck?)null);

        var sut = new DeleteTruckHandler(repository);

        var result = await sut.Handle(new DeleteTruckCommand(_testTruckId), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
