using Microsoft.AspNetCore.Mvc;
using MediatR;
using ContactFundApi.Application.Commands;
using ContactFundApi.Application.Queries;
using ContactFundApi.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ContactFundApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ContactDto>> GetById(int id)
    {
        var contact = await _mediator.Send(new GetContactByIdQuery(id));
        if (contact == null)
            return NotFound();
        
        return Ok(contact);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ContactDto>> Create(CreateContactDto createContactDto)
    {
        var contact = await _mediator.Send(new CreateContactCommand(createContactDto));
        return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ContactDto>> Update(int id, UpdateContactDto updateContactDto)
    {
        var contact = await _mediator.Send(new UpdateContactCommand(id, updateContactDto));
        return Ok(contact);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteContactCommand(id));
        return NoContent();
    }
}
