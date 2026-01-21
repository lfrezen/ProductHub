using Microsoft.EntityFrameworkCore;
using ProductHub.Domain.Products;
using ProductHub.Domain.Sales;

namespace ProductHub.Infrastructure.Data;

public class ProductHubDbContext : DbContext
{
  public ProductHubDbContext(DbContextOptions<ProductHubDbContext> options)
      : base(options)
  {
  }

  public DbSet<Product> Products => Set<Product>();
  public DbSet<Sale> Sales => Set<Sale>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductHubDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}
