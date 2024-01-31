using ErpModule.Infrastructure.Data;
using ErpModule.Trucks.WebApi.Tests.DataSeeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ErpModule.Trucks.WebApi.Tests;

public class WebApiApplicationFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<ErpDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // This should be set for each individual test run
                var inMemoryCollectionName = Guid.NewGuid().ToString();

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<ErpDbContext>(options =>
                {
                    options.UseInMemoryDatabase(inMemoryCollectionName);
                });
            });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        var host = builder.Build();
        host.Start();

        // Get service provider.
        var serviceProvider = host.Services;

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ErpDbContext>();

        var logger = scopedServices
            .GetRequiredService<ILogger<WebApiApplicationFactory<TProgram>>>();

        // Ensure the database is created.
        db.Database.EnsureCreated();

        TruckDataSeeder.PopulateTrucksData(db);

        return host;
    }
}
