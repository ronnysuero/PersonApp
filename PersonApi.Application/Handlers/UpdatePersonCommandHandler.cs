using AutoMapper;
using MediatR;
using PersonApi.Application.Commands;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Application.Handlers;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);

        if (person is null)
            throw new KeyNotFoundException($"Person with Id {request.Id} not found.");

        _mapper.Map(request, person);
        await _personRepository.UpdateAsync(person, cancellationToken);
    }
}
