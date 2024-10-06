using PersonApi.Domain.Entities;

namespace PersonApi.Database;

public class MyDbContext : DbContext
{
    protected readonly string ConnectionString;

    public MyDbContext(DbContextOptions options, string connectionString) : base(options)
    {
        ConnectionString = connectionString;
    }

    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
