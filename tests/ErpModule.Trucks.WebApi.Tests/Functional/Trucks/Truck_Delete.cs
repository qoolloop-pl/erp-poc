using System.Net;
using System.Net.Http.Json;
using ErpModule.Trucks.UseCases;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;

namespace ErpModule.Trucks.WebApi.Tests.Functional.Trucks;

public class Truck_Delete: IClassFixture<WebApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Truck_Delete(WebApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Delete_DeletesWhenExists()
    {
        var result = await _client.DeleteAsync($"/api/trucks/{TruckDataSeeder.TruckOne.Id:D}");

        Assert.True(result.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Delete_ReturnNotFoundWhenNotExists()
    {

        var result = await _client.DeleteAsync($"/api/trucks/{TruckDataSeeder.IdOfNotExistingTruck:D}");

        Assert.False(result.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
