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

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
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

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Product>> GetProductsWithoutSalesSinceAsync(
        DateTime date,
        CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(p => !p.Sales.Any(s => s.SaleDate >= date))
            .ToListAsync(cancellationToken);
    }
}
