using Ardalis.Specification;
using ErpModule.Shared;
using ErpModule.Shared.Specification;
using ErpModule.Shared.Specification.List;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.Core.Filters;
using ErpModule.Trucks.UseCases.List;
using NSubstitute;

namespace ErpModule.Trucks.UseCases.Tests.Units.List;

public class ListTruckHandler_Tests
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
        var pagedTrucks =
            new PagedList<Truck>(new List<Truck> { CreateTruck() }, new Pagination(new ListFilterBase(), 1));

        var repository = Substitute.For<IRepository<Truck>>();
        repository.ListPagedAsync(Arg.Any<ISpecification<Truck>>(), Arg.Any<ListFilterBase>(), Arg.Any<CancellationToken>())
            .Returns(pagedTrucks);

        var sut = new ListTrucksHandler(repository);

        var result = await sut.Handle(new ListTrucksQuery(new TruckListFilter()), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}
