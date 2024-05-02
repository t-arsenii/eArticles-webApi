

using eArticles.API.Data;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Services;

public class CalculateAverageRatingService : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly IServiceProvider _serviceProvider;

    public CalculateAverageRatingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CalculateAverageRating, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void CalculateAverageRating(object? state)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<eArticlesDbContext>()!;
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CalculateAverageRatingService>>();
        logger.LogInformation("Average rating for articles: calculating");
        var articles = dbContext.Articles.Include(e => e.Ratings);
        foreach (var article in articles)
        {
            if (article.Ratings.Any())
            {
                var averageRating = article.Ratings.Average(rating => rating.Value);
                article.AverageRating = averageRating;
            }
        }
        dbContext.SaveChanges();
    }
    public void Dispose()
    {
        _timer?.Dispose();
    }
}
