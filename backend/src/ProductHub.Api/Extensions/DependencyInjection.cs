using Microsoft.EntityFrameworkCore;
using ProductHub.Api.Auth;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Application.Auth;
using ProductHub.Application.Auth.Login;
using ProductHub.Application.Products.CreateProduct;
using ProductHub.Application.Products.DeleteProduct;
using ProductHub.Application.Products.GetProductById;
using ProductHub.Application.Products.GetProducts;
using ProductHub.Application.Products.RegisterSale;
using ProductHub.Application.Products.UpdateProduct;
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

        #region Application
        services.AddScoped<LoginService>();
        
        services.AddScoped<GetProductsService>();
        services.AddScoped<GetProductByIdService>();
        services.AddScoped<CreateProductService>();
        services.AddScoped<UpdateProductService>();
        services.AddScoped<DeleteProductService>();
        services.AddScoped<RegisterSaleService>();


        #endregion

        #region Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        #endregion

        #region Auth
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<TokenService>();
        #endregion

        #region Background Services
        services.AddHostedService<ProductStockMonitorService>();
        #endregion
        
        return services;
    }
}
