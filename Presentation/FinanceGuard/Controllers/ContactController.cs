using Domain.FinanceGuard.Model.Commands;
using Domain.FinanceGuard.Model.Entities;
using Domain.FinanceGuard.Model.Queries;
using Domain.FinanceGuard.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.FinanceGuard.Resources;
using Presentation.FinanceGuard.Transform;

namespace Presentation.FinanceGuard.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContactController(IContactQueryService contactQueryService, IContactCommandService contactCommandService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Contact>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var query = new GetAllContactsQuery();
            var contacts = await contactQueryService.Handle(query);

            if (contacts != null && !contacts.Any())
                return NotFound();

            var resources = contacts
                .Select(ContactResourceFromEntityAssembler.ToResourceFromEntity)
                .ToList();

            return Ok(resources);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Contact), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetContactByIdQuery(id);
        var contact = await contactQueryService.Handle(query);

        if (contact == null)
            return NotFound();

        var resource = ContactResourceFromEntityAssembler.ToResourceFromEntity(contact);

        return Ok(resource);
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreateContactResource createContactResource)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid data.");

        var command = CreateContactCommandFromResourceAssembler
            .ToCommandFromResource(createContactResource);

        var result = await contactCommandService.Handle(command);

        return CreatedAtAction(nameof(GetById), new { id = result }, new { data = result });
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContactResource updateContactResource)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid resource data.");

        var command = UpdateContactCommandFromResourceAssembler
            .ToCommandFromResource(id, updateContactResource);

        var result = await contactCommandService.Handle(command);

        return result ? NoContent() : NotFound();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteContactCommand(id);
        var result = await contactCommandService.Handle(command);

        return result ? NoContent() : NotFound();
    }
}