using ProductHub.Application.Abstractions.Repositories;

namespace ProductHub.Application.Products.GetProducts;

public class GetProductsService
{
    private readonly IProductRepository _productRepository;

    public GetProductsService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<ProductListItemDto>> ExecuteAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository
            .GetAllAsync(cancellationToken);

        return products
            .Select(p => new ProductListItemDto(
                p.Id,
                p.Name,
                p.Price,
                p.Quantity,
                p.IsOutOfStock
            ))
            .ToList();
    }
}
