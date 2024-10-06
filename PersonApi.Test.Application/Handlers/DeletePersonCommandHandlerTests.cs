using Moq;
using PersonApi.Application.Commands;
using PersonApi.Application.Handlers;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Test.Application.Handlers;

public class DeletePersonCommandHandlerTests
{
    private readonly Mock<IPersonRepository> _mockRepo;
    private readonly DeletePersonCommandHandler _handler;

    public DeletePersonCommandHandlerTests()
    {
        _mockRepo = new Mock<IPersonRepository>();
        _handler = new DeletePersonCommandHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeletePerson()
    {
        // Arrange
        var command = new DeletePersonCommand(1);
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };

        _mockRepo.Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(person);
        _mockRepo.Setup(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenPersonNotFound()
    {
        // Arrange
        var command = new DeletePersonCommand(1);

        _mockRepo.Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null!);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
