using System.Net;
using System.Net.Http.Json;
using ErpModule.Trucks.UseCases;
using ErpModule.Trucks.WebApi.Modules.Trucks.Models;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;

namespace ErpModule.Trucks.WebApi.Tests.Functional.Trucks;

public class Truck_ChangeCode: IClassFixture<WebApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Truck_ChangeCode(WebApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ChangeCode_ProperData_ReturnsOk()
    {
        var response = await _client.PostAsync($"/api/trucks/{TruckDataSeeder.TruckOne.Id:D}/code/newCode", null);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task ChangeCode_UnknownTruck_ReturnsNotFound()
    {
        var response = await _client.PostAsync($"/api/trucks/{TruckDataSeeder.IdOfNotExistingTruck:D}/code/newCode", null);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
