using System.Net;
using System.Net.Http.Json;
using ErpModule.Trucks.UseCases;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;

namespace ErpModule.Trucks.WebApi.Tests.Functional.Trucks;

public class Truck_GetOne: IClassFixture<WebApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Truck_GetOne(WebApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOne_ReturnWhenExists()
    {
        var result = await _client.GetFromJsonAsync<TruckDto>($"/api/trucks/{TruckDataSeeder.TruckOne.Id:D}");

        Assert.NotNull(result);
        Assert.Equal(TruckDataSeeder.TruckOne.Id, result.Id);
    }

    [Fact]
    public async Task GetOne_ReturnNotFoundWhenNotExists()
    {

        var result = await _client.GetAsync($"/api/trucks/{TruckDataSeeder.IdOfNotExistingTruck:D}");

        Assert.False(result.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
