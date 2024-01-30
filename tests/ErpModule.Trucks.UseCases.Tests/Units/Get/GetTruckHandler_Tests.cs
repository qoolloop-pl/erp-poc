using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.UseCases.Get;
using NSubstitute;

namespace ErpModule.Trucks.UseCases.Tests.Units.Get;

public class GetTruckHandler_Tests
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

        var sut = new GetTruckHandler(repository);

        var result = await sut.Handle(new GetTruckQuery(_testTruckId), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(_testTruckId, result.Value.Id);
    }

    [Fact]
    public async Task Handle_ReturnsNotFoundOnNoTruckInRepo()
    {
        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns((Truck?)null);

        var sut = new GetTruckHandler(repository);

        var result = await sut.Handle(new GetTruckQuery(_testTruckId), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
