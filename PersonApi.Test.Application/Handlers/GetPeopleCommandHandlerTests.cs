using AutoMapper;
using Moq;
using PersonApi.Application.Commands;
using PersonApi.Application.Handlers;
using PersonApi.Domain.Dto;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Test.Application.Handlers;

public class GetPeopleCommandHandlerTests
{
    private readonly Mock<IPersonRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetPeopleCommandHandler _handler;

    public GetPeopleCommandHandlerTests()
    {
        _mockRepo = new Mock<IPersonRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetPeopleCommandHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfPersonDto()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person
            {
                Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.doe@example.com"
            },
            new Person
            {
                Id = 2, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1992, 2, 2),
                Email = "jane.doe@example.com"
            }
        };

        var peopleDto = new List<PersonDto>
        {
            new PersonDto
            {
                Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.doe@example.com"
            },
            new PersonDto
            {
                Id = 2, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1992, 2, 2),
                Email = "jane.doe@example.com"
            }
        };

        _mockRepo.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(people);
        _mockMapper.Setup(m => m.Map<IReadOnlyList<PersonDto>>(people)).Returns(peopleDto);

        // Act
        var result = await _handler.Handle(new GetPeopleCommand(), CancellationToken.None);

        // Assert
        Assert.Equal(peopleDto, result);
        _mockRepo.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
