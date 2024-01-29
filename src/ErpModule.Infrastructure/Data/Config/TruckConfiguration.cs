using ErpModule.Trucks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpModule.Infrastructure.Data.Config;

public class TruckConfiguration: IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.HasIndex(truck => truck.Code).IsUnique();
        builder.Property(truck => truck.Status)
            .HasConversion(
                status => status.Value,
                status => TruckStatus.FromValue(status));
    }
}
