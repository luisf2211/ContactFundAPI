using Microsoft.AspNetCore.Mvc;
using MediatR;
using ContactFundApi.Application.Commands;
using ContactFundApi.Application.Queries;
using ContactFundApi.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ContactFundApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FundsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FundsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<FundDto>>> GetAll()
    {
        var funds = await _mediator.Send(new GetAllFundsQuery());
        return Ok(funds);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<FundDto>> GetById(int id)
    {
        var fund = await _mediator.Send(new GetFundByIdQuery(id));
        if (fund == null)
            return NotFound();
        
        return Ok(fund);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<FundDto>> Create(CreateFundDto createFundDto)
    {
        var fund = await _mediator.Send(new CreateFundCommand(createFundDto));
        return CreatedAtAction(nameof(GetById), new { id = fund.Id }, fund);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<FundDto>> Update(int id, UpdateFundDto updateFundDto)
    {
        var fund = await _mediator.Send(new UpdateFundCommand(id, updateFundDto));
        return Ok(fund);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteFundCommand(id));
        return NoContent();
    }

    [HttpPost("{fundId}/contacts/{contactId}/assign")]
    [Authorize]
    public async Task<IActionResult> AssignContact(int fundId, int contactId)
    {
        await _mediator.Send(new AssignContactCommand(contactId, fundId));
        return Ok();
    }

    [HttpDelete("{fundId}/contacts/{contactId}/unassign")]
    [Authorize]
    public async Task<IActionResult> UnassignContact(int fundId, int contactId)
    {
        await _mediator.Send(new UnassignContactCommand(contactId, fundId));
        return NoContent();
    }

    [HttpGet("{fundId}/contacts")]
    [Authorize]
    public async Task<IActionResult> GetContactsForFund(int fundId)
    {
        var contacts = await _mediator.Send(new GetContactsForFundQuery(fundId));
        return Ok(contacts);
    }
}
