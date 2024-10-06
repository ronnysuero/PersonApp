using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PersonApi.Database;

public class MigrationService<T> : IHostedService where T : DbContext
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MigrationService<T>> _logger;

    public MigrationService(IServiceProvider serviceProvider, ILogger<MigrationService<T>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting migration service");

#if RELEASE
        // Delay execution for 10 seconds, so the database is up and running
        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
#endif

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();

        if (!dbContext.Database.IsInMemory())
            await dbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping migration service");
        return Task.CompletedTask;
    }
}
