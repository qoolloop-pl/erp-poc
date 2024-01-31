using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ErpModule.Infrastructure;

public static class DataMigrationServiceProviderExtension
{
    public static async Task MigrateContext(this IServiceProvider provider, params Type[] contexts)
    {
        var logger = provider.GetRequiredService<ILogger<DbContext>>();
        foreach (var contextType in contexts)
        {
            using var scope = provider.CreateScope();
            var services = scope.ServiceProvider;
            await using var dbContext = services.GetRequiredService(contextType) as DbContext;

            if (dbContext is null)
            {
                logger.LogInformation($"type [{contextType.Name}] is not DbContext");
                continue;
            }

            if (dbContext.Database.IsRelational() && (await dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                logger.LogInformation($"there are migrations for  [{contextType.Name}], migrating");
                await dbContext.Database.MigrateAsync();
            }
            else
            {
                logger.LogInformation($"database [{contextType.Name}] is up to date");
            }
        }
    }
}
