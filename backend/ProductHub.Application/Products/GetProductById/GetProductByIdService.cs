using ProductHub.Application.Abstractions.Repositories;

namespace ProductHub.Application.Products.GetProductById;

public class GetProductByIdService
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDetailsDto?> ExecuteAsync(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.Id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {query.Id} not found");

        return new ProductDetailsDto(
            product.Id,
            product.Name,
            product.Price,
            product.Quantity,
            product.IsOutOfStock,
            product.LastSaleDate);
    }
}