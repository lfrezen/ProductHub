using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Application.Products.CreateProduct;
using ProductHub.Application.Products.RegisterSale;
using ProductHub.Infrastructure.Data;
using ProductHub.Infrastructure.Repositories;

namespace ProductHub.Api.Extensions;

public static class DependencyInjection
{
  public static IServiceCollection AddProductHub(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<ProductHubDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    // Application
    services.AddScoped<CreateProductService>();
    services.AddScoped<RegisterSaleService>();

    // Repositories
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ISaleRepository, SaleRepository>();

    return services;
  }
}
