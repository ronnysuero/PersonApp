using AutoMapper;
using Moq;
using PersonApi.Application.Commands;
using PersonApi.Application.Handlers;
using PersonApi.Domain.Dto;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Test.Application.Handlers;

public class GetPersonCommandHandlerTests
{
    private readonly Mock<IPersonRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetPersonCommandHandler _handler;

    public GetPersonCommandHandlerTests()
    {
        _mockRepo = new Mock<IPersonRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetPersonCommandHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPersonDto()
    {
        // Arrange
        var command = new GetPersonCommand(1);
        var person = new Person
        {
            Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com"
        };
        var personDto = new PersonDto
        {
            Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com"
        };

        _mockRepo.Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(person);
        _mockMapper.Setup(m => m.Map<PersonDto>(person)).Returns(personDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(personDto, result);
        _mockRepo.Verify(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }
}
