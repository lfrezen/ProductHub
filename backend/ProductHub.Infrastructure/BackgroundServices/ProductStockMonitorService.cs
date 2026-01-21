using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductHub.Application.Abstractions.Repositories;

namespace ProductHub.Infrastructure.BackgroundServices;

public class ProductStockMonitorService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ProductStockMonitorService> _logger;
    private readonly IConfiguration _configuration;

    public ProductStockMonitorService(
        IServiceScopeFactory scopeFactory,
        ILogger<ProductStockMonitorService> logger,
        IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var intervalHours = _configuration.GetValue<double>("BackgroundServices:StockMonitorIntervalHours");
        var daysWithoutSales = _configuration.GetValue<int>("BackgroundServices:DaysWithoutSalesToMarkOutOfStock");

        _logger.LogInformation(
            "Product Stock Monitor Service is starting (Interval: {Interval}h, Days: {Days})",
            intervalHours,
            daysWithoutSales);

        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessAsync(daysWithoutSales, stoppingToken);

            _logger.LogInformation("Next execution scheduled in {Hours} hours", intervalHours);
            await Task.Delay(TimeSpan.FromHours(intervalHours), stoppingToken);
        }

        _logger.LogInformation("Product Stock Monitor Service is stopping");
    }

    private async Task ProcessAsync(int daysWithoutSales, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting stock monitoring process...");

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider
                .GetRequiredService<IProductRepository>();

            var limitDate = DateTime.UtcNow.AddDays(-daysWithoutSales);
            var products = await repository
                .GetProductsWithoutSalesSinceAsync(limitDate, cancellationToken);

            var productsList = products.ToList();
            _logger.LogInformation("Found {Count} products to mark as out of stock", productsList.Count);

            foreach (var product in productsList)
            {
                product.MarkAsOutOfStock();
                await repository.UpdateAsync(product, cancellationToken);
            }

            _logger.LogInformation("Stock monitoring process completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during stock monitoring process");
        }
    }
}