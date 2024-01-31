using ErpModule.Infrastructure.Data;
using ErpModule.Trucks.Core;

namespace ErpModule.Trucks.WebApi.Tests.DataSeeders;

public class TruckDataSeeder
{
    public static readonly Guid IdOfNotExistingTruck = new Guid("a0c0e6d7-c35e-431e-b339-1f6885d32380");

    public static readonly int TestTrucksCount = 4;

    public static readonly Truck TruckOne = new Truck("code1", "name1", "description 1")
        { Id = new Guid("75624a50-494a-4cbd-a262-ed1a41e1ab88") };

    public static readonly Truck TruckTwo = new Truck("code2", "name2", "description 2")
        { Id = new Guid("85624a50-494a-4cbd-a262-ed1a41e1ab88") };

    public static readonly Truck TruckThree = new Truck("codex_1", "namex 1", "description 3")
        { Id = new Guid("95624a50-494a-4cbd-a262-ed1a41e1ab88") };

    public static readonly Truck TruckFour= new Truck("codex_2", "namex 2", "description 4")
        { Id = new Guid("a5624a50-494a-4cbd-a262-ed1a41e1ab88") };

    public static void PopulateTrucksData(ErpDbContext db)
    {
        foreach (var truck in db.Trucks)
        {
            db.Trucks.Remove(truck);
        }

        db.SaveChanges();

        db.Trucks.Add(TruckOne);
        db.Trucks.Add(TruckTwo);
        db.Trucks.Add(TruckThree);
        db.Trucks.Add(TruckFour);

        db.SaveChanges();
    }
}
