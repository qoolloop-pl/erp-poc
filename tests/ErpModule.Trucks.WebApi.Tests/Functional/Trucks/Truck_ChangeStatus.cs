using System.Net;
using System.Net.Http.Json;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.UseCases;
using ErpModule.Trucks.WebApi.Modules.Trucks.Models;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;

namespace ErpModule.Trucks.WebApi.Tests.Functional.Trucks;

public class Truck_ChangeStatus: IClassFixture<WebApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Truck_ChangeStatus(WebApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ChangeStatus_Valid_ReturnsOk()
    {
        var response = await _client.PostAsync($"/api/trucks/{TruckDataSeeder.TruckOne.Id:D}/status/{TruckStatus.Returning}", null);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task ChangeStatus_ValidationFails_OnBadRequest()
    {
        var response = await _client.PostAsync($"/api/trucks/{TruckDataSeeder.TruckOne.Id:D}/status/nonexistingstatus", null);
        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateDetails_OnBadStatusChange_ReturnsBadRequest()
    {
        //change to returning which should be fine
        var response = await _client.PostAsync($"/api/trucks/{TruckDataSeeder.TruckTwo.Id:D}/status/{TruckStatus.Returning}", null);
        Assert.True(response.IsSuccessStatusCode);


        //then to bad one
        response = await _client.PostAsync($"/api/trucks/{TruckDataSeeder.TruckTwo.Id:D}/status/{TruckStatus.AtJob}", null);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateDetails_OnNonExistingId_ReturnsNotFound()
    {
        var response = await _client.PostAsync($"/api/trucks/{TruckDataSeeder.IdOfNotExistingTruck:D}/status/{TruckStatus.AtJob}", null);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
