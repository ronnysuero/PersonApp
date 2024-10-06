using MediatR;
using PersonApi.Application.Commands;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Application.Handlers;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository) =>
        _personRepository = personRepository;

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);

        if (person is null)
            throw new KeyNotFoundException($"Person with Id {request.Id} not found.");

        await _personRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
