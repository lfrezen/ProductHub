using Microsoft.EntityFrameworkCore;
using ProductHub.Api.Auth;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Application.Auth;
using ProductHub.Application.Auth.Login;
using ProductHub.Application.Products.CreateProduct;
using ProductHub.Application.Products.RegisterSale;
using ProductHub.Infrastructure.Auth;
using ProductHub.Infrastructure.BackgroundServices;
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
        services.AddScoped<LoginService>();
        services.AddScoped<RegisterSaleService>();

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        // Auth
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<TokenService>();

        // Background Services
        services.AddHostedService<ProductStockMonitorService>();

        return services;
    }
}
