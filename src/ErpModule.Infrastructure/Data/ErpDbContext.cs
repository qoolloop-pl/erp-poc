using System.Reflection;
using ErpModule.Trucks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ErpModule.Infrastructure.Data;

public class ErpDbContext: DbContext
{
    public DbSet<Truck> Trucks { get; set; }

    public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public class ErpDbContextFactory : IDesignTimeDbContextFactory<ErpDbContext>
{
    public ErpDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ErpDbContext>();
        optionsBuilder.UseSqlite("Data Source=app.db");

        return new ErpDbContext(optionsBuilder.Options);
    }
}
