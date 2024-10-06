using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonApi.Database.MySql;

namespace PersonApi.Test.Integration.Fakes;

public class FakeMySqlDbContext : MySqlDbContext
{
    public FakeMySqlDbContext(DbContextOptions<MySqlDbContext> options, IConfiguration configuration) :
        base(options, configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("InMemoryDb");
        OnConfigured(optionsBuilder);
    }

    public void Seed()
    {
        if (People.Any(a => a.Id == FakePeople.Person.Id))
            return;

        People.Add(FakePeople.Person);
        SaveChanges();
    }
}
