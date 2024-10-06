using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Application.Commands;
using PersonApi.Domain.Dto;

namespace PersonApi.Controllers;

[Route("api/[controller]"), ApiController]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll() =>
        Ok(await _mediator.Send(new GetPeopleCommand()));

    [HttpGet("{id:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<PersonDto>> GetById(int id)
    {
        var person = await _mediator.Send(new GetPersonCommand(id));
        return person is null ? NotFound() : Ok(person);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<int>> Create([FromBody] CreatePersonCommand command)
    {
        var personId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = personId }, personId);
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePersonCommand(id));
        return NoContent();
    }
}
