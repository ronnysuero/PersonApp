using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PersonApi.Database.MySql;
using PersonApi.Test.Integration.Fakes;

namespace PersonApi.Test.Integration;

[ExcludeFromCodeCoverage]
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(MySqlDbContext));

            if (dbContext != null)
            {
                services.Remove(dbContext);
                services.AddDbContext<MySqlDbContext, FakeMySqlDbContext>();
            }
        });

        builder.UseEnvironment("Development");
    }
}
