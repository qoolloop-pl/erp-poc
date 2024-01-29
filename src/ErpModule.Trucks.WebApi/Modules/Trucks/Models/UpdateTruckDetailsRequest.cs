using System.ComponentModel.DataAnnotations;

namespace ErpModule.Trucks.WebApi.Modules.Trucks.Models;

public class UpdateTruckDetailsRequest
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}
