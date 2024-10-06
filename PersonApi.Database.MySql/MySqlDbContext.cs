using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PersonApi.Database.MySql;

public class MySqlDbContext : MyDbContext
{
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options, IConfiguration configuration) :
        base(options, configuration.GetConnectionString("DefaultConnection"))
    {
    }


    [ExcludeFromCodeCoverage]
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString), mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                );
            }
        );

        OnConfigured(optionsBuilder);
    }

    protected void OnConfigured(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);
}
