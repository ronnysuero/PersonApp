using MediatR;

namespace PersonApi.Application.Commands;

public record DeletePersonCommand(int Id) : IRequest
{
}
