using AutoMapper;
using MediatR;
using PersonApi.Application.Commands;
using PersonApi.Domain.Dto;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Application.Handlers;

public class GetPersonCommandHandler : IRequestHandler<GetPersonCommand, PersonDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<PersonDto> Handle(GetPersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<PersonDto>(person);
    }
}
