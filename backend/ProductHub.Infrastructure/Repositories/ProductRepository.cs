using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Domain.Products;
using ProductHub.Infrastructure.Data;

namespace ProductHub.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
  private readonly ProductHubDbContext _context;

  public ProductRepository(ProductHubDbContext context)
  {
    _context = context;
  }

  public async Task AddAsync(Product product, CancellationToken cancellationToken)
  {
    _context.Products.Add(product);
    await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await _context.Products
        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
  }

  public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
  {
    _context.Products.Update(product);
    await _context.SaveChangesAsync(cancellationToken);
  }
}
