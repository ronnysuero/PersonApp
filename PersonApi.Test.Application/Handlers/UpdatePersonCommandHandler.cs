using AutoMapper;
using Moq;
using PersonApi.Application.Commands;
using PersonApi.Application.Handlers;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Test.Application.Handlers;

public class UpdatePersonCommandHandlerTests
{
    private readonly Mock<IPersonRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UpdatePersonCommandHandler _handler;

    public UpdatePersonCommandHandlerTests()
    {
        _mockRepo = new Mock<IPersonRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new UpdatePersonCommandHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdatePerson()
    {
        // Arrange
        var command = new UpdatePersonCommand
        {
            Id = 1, FirstName = "John", LastName = "Doe", Email = "jhondoe@gmail.com", DateOfBirth = DateTime.UtcNow
        };
        var person = new Person { Id = 1, FirstName = "Jane", LastName = "Smith" };

        _mockRepo.Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(person);
        _mockMapper.Setup(m => m.Map(command, person)).Returns(person);
        _mockRepo.Setup(repo => repo.UpdateAsync(person, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateAsync(person, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenPersonNotFound()
    {
        // Arrange
        var command = new UpdatePersonCommand { Id = 1, FirstName = "John", LastName = "Doe" };

        _mockRepo.Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null!);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
