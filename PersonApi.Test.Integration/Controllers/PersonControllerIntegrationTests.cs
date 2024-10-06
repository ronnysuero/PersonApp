using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PersonApi.Application.Commands;
using PersonApi.Domain.Dto;
using PersonApi.Test.Integration.Fakes;

namespace PersonApi.Test.Integration.Controllers;

public class PersonControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PersonControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();
        var services = scope.ServiceProvider;

        services.GetRequiredService<FakeMySqlDbContext>().Seed();

        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePerson_Should_Return_Created_Response()
    {
        // Arrange
        var command = new CreatePersonCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Now.AddYears(-30),
            Email = "john.doe@example.com"
        };

        var content = new StringContent(
            JsonConvert.SerializeObject(command),
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );
        content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

        // Act
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/person")
        {
            Content = content
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetPerson_Should_Return_Ok_Response()
    {
        // Arrange
        var personId = FakePeople.Person.Id;

        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/person/{personId}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdatePerson_Should_Return_NoContent_Response()
    {
        // Arrange
        var command = new UpdatePersonCommand
        {
            Id = FakePeople.Person.Id,
            FirstName = "Jan",
            LastName = "Do",
            DateOfBirth = DateTime.Now.AddYears(-80),
            Email = "jan.do@example.com"
        };

        var content = new StringContent(
            JsonConvert.SerializeObject(command),
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );
        content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

        // Act
        var request = new HttpRequestMessage(HttpMethod.Put, $"/api/person/{command.Id}")
        {
            Content = content
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeletePerson_Should_Return_NoContent_Response()
    {
        // Arrange
        var personId = FakePeople.Person.Id;

        // Act
        var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/person/{personId}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetAllPersons_Should_Return_Ok_Response()
    {
        // Act
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/person");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var people = JsonConvert.DeserializeObject<List<PersonDto>>(content);
        Assert.NotNull(people);
        Assert.NotEmpty(people);
    }
}
