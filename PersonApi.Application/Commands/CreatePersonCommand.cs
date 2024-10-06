using MediatR;

namespace PersonApi.Application.Commands;

public record CreatePersonCommand : IRequest<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }
}
