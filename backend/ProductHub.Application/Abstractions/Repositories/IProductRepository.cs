using ProductHub.Domain.Products;

namespace ProductHub.Application.Abstractions.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<List<Product>> GetProductsWithoutSalesSinceAsync(
    DateTime date,
    CancellationToken cancellationToken);

}
