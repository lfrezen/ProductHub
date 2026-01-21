using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductHub.Application.Abstractions.Repositories;

namespace ProductHub.Infrastructure.BackgroundServices;

public class ProductStockMonitorService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    private async Task ProcessAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider
            .GetRequiredService<IProductRepository>();

        var limitDate = DateTime.UtcNow.AddDays(-10);

        var products = await repository
            .GetProductsWithoutSalesSinceAsync(limitDate, cancellationToken);

        foreach (var product in products)
        {
            product.MarkAsOutOfStock();
            await repository.UpdateAsync(product, cancellationToken);
        }
    }
}
