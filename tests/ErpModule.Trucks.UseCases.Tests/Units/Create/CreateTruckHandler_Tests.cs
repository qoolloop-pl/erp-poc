using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.UseCases.Create;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace ErpModule.Trucks.UseCases.Tests.Units.Create;

public class CreateTruckHandler_Tests
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
        repository.AddAsync(Arg.Any<Truck>(), Arg.Any<CancellationToken>())
            .Returns(CreateTruck());

        var sut = new CreateTruckHandler(repository);

        var result = await sut.Handle(new CreateTruckCommand("code", "name"), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(_testTruckId, result.Value);
    }

    [Fact]
    public async Task Handle_ReturnsErrorOnRepoError()
    {
        var repository = Substitute.For<IRepository<Truck>>();
        repository.AddAsync(Arg.Any<Truck>(), Arg.Any<CancellationToken>())
            .Throws<Exception>();

        var sut = new CreateTruckHandler(repository);

        var result = await sut.Handle(new CreateTruckCommand("code", "name"), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Error, result.Status);
    }
}
