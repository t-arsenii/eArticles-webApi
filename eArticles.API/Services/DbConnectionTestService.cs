
using eArticles.API.Data;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services;

public class DbConnectionTestService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    public DbConnectionTestService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<eArticlesDbContext>()!;
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbConnectionTestService>>()!;
        try
        {
            if(!await dbContext.Database.CanConnectAsync(cancellationToken))
            {
               logger.LogError("Database connection failed");
            }
            else
            {
                logger.LogInformation($"Database connection success: {dbContext.Database.ProviderName}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database connection failed", ex);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
