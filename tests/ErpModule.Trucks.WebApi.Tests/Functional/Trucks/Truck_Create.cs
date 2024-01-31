using System.Net;
using System.Net.Http.Json;
using ErpModule.Trucks.WebApi.Modules.Trucks.Models;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;

namespace ErpModule.Trucks.WebApi.Tests.Functional.Trucks;

public class Truck_Create: IClassFixture<WebApiApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Truck_Create(WebApiApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Crate_Creating_ReturnsGuid()
    {
        var truckRequest = new CreateTruckRequest
        {
            Code = "new code",
            Name = "new name"
        };

        var response = await _client.PostAsJsonAsync("/api/trucks", truckRequest);

        Assert.True(response.IsSuccessStatusCode);

        var id = await response.Content.ReadFromJsonAsync<Guid>();
    }

    [Fact]
    public async Task Crate_ValidationFails_ReturnsBadRequest()
    {
        var truckRequest = new CreateTruckRequest
        {
            Code = "",
            Name = "new name"
        };

        var response = await _client.PostAsJsonAsync("/api/trucks", truckRequest);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /*
    // There is infrastructure constrain in db, not available for InMemoryDB
    [Fact]
    public async Task Crate_CreatingWithExistingCode_ReturnsGuid()
    {
        var truckRequest = new CreateTruckRequest
        {
            Code = TruckDataSeeder.TruckOne.Code,
            Name = "new name"
        };

        var response = await _client.PostAsJsonAsync("/api/trucks", truckRequest);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }*/
}
