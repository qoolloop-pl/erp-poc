using ErpModule.Trucks.Core.Filters;
using ErpModule.Trucks.UseCases.Create;
using ErpModule.Trucks.UseCases.List;
using ErpModule.Trucks.WebApi.Modules.Trucks.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpModule.Trucks.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrucksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrucksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get([FromQuery] TruckListFilter filter)
    {
        var query = new ListTrucksQuery(filter);

        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Status);
    }

    [HttpPost("")]
    public async Task<IActionResult> Post(CreateTruckRequest createTruckRequest)
    {
        var result = await _mediator.Send(
            new CreateTruckCommand(
                createTruckRequest.Code,
                createTruckRequest.Name,
                createTruckRequest.Description));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Status);
    }
}
