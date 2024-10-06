using AutoMapper;
using PersonApi.Application.Commands;
using PersonApi.Domain.Dto;
using PersonApi.Domain.Entities;

namespace PersonApi.Application.MappingProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonCommand, Person>();
        CreateMap<UpdatePersonCommand, Person>();
        CreateMap<Person, PersonDto>();
    }
}
