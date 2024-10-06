using AutoMapper;
using MediatR;
using PersonApi.Application.Commands;
using PersonApi.Domain.Dto;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Application.Handlers;

public class GetPeopleCommandHandler : IRequestHandler<GetPeopleCommand, IReadOnlyList<PersonDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPeopleCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<PersonDto>> Handle(GetPeopleCommand request, CancellationToken cancellationToken)
    {
        var people = await _personRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<PersonDto>>(people);
    }
}
