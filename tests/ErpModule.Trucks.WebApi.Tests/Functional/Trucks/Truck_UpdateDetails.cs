using System.Net;
using System.Net.Http.Json;
using ErpModule.Trucks.UseCases;
using ErpModule.Trucks.WebApi.Modules.Trucks.Models;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;

namespace ErpModule.Trucks.WebApi.Tests.Functional.Trucks;

public class Truck_UpdateDetails: IClassFixture<WebApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Truck_UpdateDetails(WebApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateDetails_Update_ReturnsDto()
    {
        var updateTruckDetailsRequest = new UpdateTruckDetailsRequest()
        {
            Name = "new name",
            Description = "new description"
        };

        var response = await _client.PostAsJsonAsync($"/api/trucks/{TruckDataSeeder.TruckOne.Id:D}", updateTruckDetailsRequest);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateDetails_ValidationFails_OnBadRequest()
    {
        var updateTruckDetailsRequest = new UpdateTruckDetailsRequest()
        {
            Name = "",
            Description = "new description"
        };

        var response = await _client.PostAsJsonAsync($"/api/trucks/{TruckDataSeeder.TruckOne.Id:D}", updateTruckDetailsRequest);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateDetails_ReturnNotFoundWhenNotExists()
    {
        var updateTruckDetailsRequest = new UpdateTruckDetailsRequest()
        {
            Name = "new name",
            Description = "new description"
        };

        var response = await _client.PostAsJsonAsync($"/api/trucks/{TruckDataSeeder.IdOfNotExistingTruck}", updateTruckDetailsRequest);

        Assert.False(response.IsSuccessStatusCode);
    }
}
