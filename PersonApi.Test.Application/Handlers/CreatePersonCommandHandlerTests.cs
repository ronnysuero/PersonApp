using AutoMapper;
using Moq;
using PersonApi.Application.Commands;
using PersonApi.Application.Handlers;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Test.Application.Handlers;

public class CreatePersonCommandHandlerTests
{
    private readonly Mock<IPersonRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreatePersonCommandHandler _handler;

    public CreatePersonCommandHandlerTests()
    {
        _mockRepo = new Mock<IPersonRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreatePersonCommandHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreatePerson()
    {
        // Arrange
        var command = new CreatePersonCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com"
        };
        var person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com"
        };

        _mockMapper.Setup(m => m.Map<Person>(command)).Returns(person);
        _mockRepo.Setup(repo => repo.AddAsync(person, It.IsAny<CancellationToken>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(person.Id, result);
        _mockRepo.Verify(repo => repo.AddAsync(person, It.IsAny<CancellationToken>()), Times.Once);
    }
}
