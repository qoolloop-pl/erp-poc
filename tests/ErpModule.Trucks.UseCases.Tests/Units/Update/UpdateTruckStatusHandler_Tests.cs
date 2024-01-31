using System.Reflection;
using Ardalis.Result;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.UseCases.Get;
using ErpModule.Trucks.UseCases.Update;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace ErpModule.Trucks.UseCases.Tests.Units.Update;

public class UpdateTruckStatusHandler_Tests
{
    private readonly Guid _testTruckId = new Guid("9CC867D3-3A7D-4794-BB94-1E4F4EB3BA1D");

    private Truck CreateTruck(TruckStatus? withStatus = null)
    {
        var truck = new Truck("code", "name", "description")
        {
            Id = _testTruckId
        };

        if (withStatus is not null && truck.Status.CanMoveTo(withStatus))
        {
            truck.ChangeStatus(withStatus);
        }
        return truck;
    }

    [Fact]
    public async Task Handle_ReturnsSuccessOnValidCommand()
    {
        var newStatus = TruckStatus.OutOfService;
        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(CreateTruck());

        var sut = new UpdateTruckStatusHandler(repository);

        var result = await sut.Handle(new UpdateTruckStatusCommand(_testTruckId, newStatus), CancellationToken.None);

        Assert.True(result.IsSuccess);
        await repository.Received().UpdateAsync(Arg.Is<Truck>(t => t.Status.Equals(newStatus)), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ReturnsNotFoundOnNoTruckInRepo()
    {
        var newStatus = TruckStatus.OutOfService;
        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns((Truck?)null);

        var sut = new UpdateTruckStatusHandler(repository);

        var result = await sut.Handle(new UpdateTruckStatusCommand(_testTruckId, newStatus), Arg.Any<CancellationToken>());

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }

    [Fact]
    public async Task Handle_ReturnsForbiddenOnBadNewStatus()
    {
        var newBadStatus = TruckStatus.AtJob;
        var repository = Substitute.For<IRepository<Truck>>();
        repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(CreateTruck(TruckStatus.Returning));

        var sut = new UpdateTruckStatusHandler(repository);

        var result = await sut.Handle(new UpdateTruckStatusCommand(_testTruckId, newBadStatus), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.Forbidden, result.Status);
    }
}
