using System.ComponentModel.DataAnnotations;

namespace ErpModule.Trucks.WebApi.Modules.Trucks.Models;

public class CreateTruckRequest
{
    [Required]
    [MinLength(3)]
    public string Code { get; set; }
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
    public string? Description { get; set; }
}
