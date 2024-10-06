using MediatR;
using PersonApi.Domain.Dto;

namespace PersonApi.Application.Commands;

public record GetPersonCommand(int Id) : IRequest<PersonDto>
{
}
