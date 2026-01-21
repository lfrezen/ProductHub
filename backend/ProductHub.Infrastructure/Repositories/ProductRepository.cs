using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Domain.Products;
using ProductHub.Infrastructure.Data;

namespace ProductHub.Infrastructure.Repositories;

public class ProductRepository(ProductHubDbContext context) : IProductRepository
{
    private readonly ProductHubDbContext _context = context;

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

    public async Task<List<Product>> GetProductsWithoutSalesSinceAsync(
    DateTime date,
    CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(p => p.LastSaleDate == null || p.LastSaleDate < date)
            .Where(p => !p.IsOutOfStock)
            .ToListAsync(cancellationToken);
    }
}
