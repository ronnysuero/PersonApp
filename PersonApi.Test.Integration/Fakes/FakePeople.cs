using PersonApi.Domain.Entities;

namespace PersonApi.Test.Integration.Fakes;

public class FakePeople
{
    public static Person Person = new()
    {
        Id = 1,
        FirstName = "Jane",
        LastName = "Doe",
        DateOfBirth = DateTime.Now.AddYears(-25),
        Email = "jane.doe@example.com"
    };
}
