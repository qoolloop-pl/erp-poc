using System.Net.Http.Json;
using ErpModule.Shared.Specification;
using ErpModule.Trucks.UseCases;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ErpModule.Trucks.WebApi.Tests.Functional.Trucks;

public class Truck_GetList: IClassFixture<WebApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Truck_GetList(WebApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsAllTrucks()
    {
        var allTrucks = await _client.GetFromJsonAsync<PagedList<TruckDto>>("/api/trucks");

        Assert.NotNull(allTrucks);
        Assert.NotEmpty(allTrucks.Data);
        Assert.Equal(TruckDataSeeder.TestTrucksCount, allTrucks.Data.Count);
    }

    [Fact]
    public async Task Get_FilterByCodeNotExistingCode_ReturnEmpty()
    {
        var allTrucks = await _client.GetFromJsonAsync<PagedList<TruckDto>>("/api/trucks?Code=nonexisting");

        Assert.NotNull(allTrucks);
        Assert.Empty(allTrucks.Data);
        Assert.Equal(0, allTrucks.Page.TotalItems);
    }

    [Fact]
    public async Task Get_FilterByCodeExactCode_ReturnEmpty()
    {
        var allTrucks = await _client.GetFromJsonAsync<PagedList<TruckDto>>($"/api/trucks?Code={TruckDataSeeder.TruckOne.Code}");

        Assert.NotNull(allTrucks);
        Assert.NotEmpty(allTrucks.Data);
        Assert.Single(allTrucks.Data);
        Assert.Equal(1, allTrucks.Page.TotalItems);
    }
}
