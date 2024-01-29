using System.Runtime.InteropServices;
using Ardalis.Result;
using ErpModule.Shared.Specification;
using ErpModule.Trucks.Core;
using ErpModule.Trucks.Core.Filters;
using ErpModule.Trucks.UseCases;
using ErpModule.Trucks.UseCases.Create;
using ErpModule.Trucks.UseCases.Delete;
using ErpModule.Trucks.UseCases.Get;
using ErpModule.Trucks.UseCases.List;
using ErpModule.Trucks.UseCases.Update;
using ErpModule.Trucks.WebApi.Modules.Trucks.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpModule.Trucks.WebApi.Modules.Trucks.Controllers;

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
    [ProducesResponseType<PagedList<TruckDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTruckRequest createTruckRequest)
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

    [HttpGet("{id:guid}")]
    [ProducesResponseType<TruckDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTruckQuery(id));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        return BadRequest(result.Status);
    }

    [HttpPost("{id:guid}")]
    [ProducesResponseType<TruckDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDetails(Guid id, [FromBody] UpdateTruckDetailsRequest request)
    {
        var result = await _mediator.Send(new UpdateTruckCommand(
            id,
            request.Name,
            request.Description));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        return BadRequest(result.Status);
    }

    [HttpPost("{id:guid}/code/{code:required}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeCode(Guid id, string code)
    {
        var result = await _mediator.Send(new UpdateTruckCodeCommand(id, code));

        if (result.IsSuccess)
        {
            return Ok();
        }

        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        return BadRequest(result.Status);
    }

    [HttpPost("{id:guid}/status/{status:required}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeStatus(Guid id, string status)
    {
        if (!TruckStatus.TryFromName(status, out var nextStatus))
        {
            return BadRequest("status is not correct");
        }

        var result = await _mediator.Send(new UpdateTruckStatusCommand(
            id,
            nextStatus));

        if (result.IsSuccess)
        {
            return Ok();
        }

        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        if (result.Status == ResultStatus.Forbidden)
        {
            return BadRequest($"cannot change current status to {nextStatus.Name}");
        }

        return BadRequest(result.Status);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteTruckCommand(id));

        if (result.IsSuccess)
        {
            return Ok();
        }

        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        return BadRequest(result.Status);
    }
}
