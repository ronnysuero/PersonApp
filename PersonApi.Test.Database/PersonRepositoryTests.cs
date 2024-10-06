using Microsoft.EntityFrameworkCore;
using PersonApi.Database;
using PersonApi.Database.Repositories;
using PersonApi.Domain.Entities;

namespace PersonApi.Test.Database;

public class PersonRepositoryTests
{
    private static MyDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new MyDbContext(options, "connectionString");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPerson()
    {
        // Arrange
        const int personId = 1;
        var person = new Person { Id = personId, FirstName = "John", LastName = "Doe" };
        var context = GetDbContext();
        context.People.Add(person);
        await context.SaveChangesAsync();
        var repository = new PersonRepository(context);

        // Act
        var result = await repository.GetByIdAsync(personId, CancellationToken.None);

        // Assert
        Assert.Equal(personId, result?.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPeople()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person { Id = 1, FirstName = "John", LastName = "Doe" },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
        var context = GetDbContext();
        context.People.AddRange(people);
        await context.SaveChangesAsync();
        var repository = new PersonRepository(context);

        // Act
        var result = await repository.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.Equal(people.Count, result.Count());
    }

    [Fact]
    public async Task AddAsync_ShouldAddPerson()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
        var context = GetDbContext();
        var repository = new PersonRepository(context);

        // Act
        await repository.AddAsync(person, CancellationToken.None);

        // Assert
        var result = await context.People.FindAsync(person.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdatePerson()
    {
        // Arrange
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
        var context = GetDbContext();
        var repository = new PersonRepository(context);
        context.People.Add(person);
        await context.SaveChangesAsync();

        person.FirstName = "Jane";

        // Act
        await repository.UpdateAsync(person, CancellationToken.None);

        // Assert
        var result = await context.People.FindAsync(person.Id);
        Assert.Equal("Jane", result?.FirstName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeletePerson()
    {
        // Arrange
        var personId = 1;
        var person = new Person { Id = personId, FirstName = "John", LastName = "Doe" };
        var context = GetDbContext();
        context.People.Add(person);
        await context.SaveChangesAsync();
        var repository = new PersonRepository(context);
        // Act
        await repository.DeleteAsync(personId, CancellationToken.None);

        // Assert
        var result = await context.People.FindAsync(personId);
        Assert.Null(result);
    }
}
