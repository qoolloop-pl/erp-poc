using ErpModule.Infrastructure.Data;
using ErpModule.Shared;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace ErpModule.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        RegisterEf(services);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<Truck>();
            configuration.RegisterServicesFromAssemblyContaining<TruckDto>();
        });

        return services;
    }

    private static void RegisterEf(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }
}
