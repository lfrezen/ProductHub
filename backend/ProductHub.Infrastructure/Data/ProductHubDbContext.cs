using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Auth;
using ProductHub.Domain.Products;
using ProductHub.Domain.Sales;

namespace ProductHub.Infrastructure.Data;

public class ProductHubDbContext(DbContextOptions<ProductHubDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
