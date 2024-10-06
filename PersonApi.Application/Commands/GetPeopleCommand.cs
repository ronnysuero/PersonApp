using MediatR;
using PersonApi.Domain.Dto;

namespace PersonApi.Application.Commands;

public record GetPeopleCommand : IRequest<IReadOnlyList<PersonDto>>
{
}
