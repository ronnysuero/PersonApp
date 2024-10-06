using AutoMapper;
using MediatR;
using PersonApi.Application.Commands;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Application.Handlers;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);
        await _personRepository.AddAsync(person, cancellationToken);
        return person.Id;
    }
}
