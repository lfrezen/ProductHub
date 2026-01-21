using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Domain.Products;

namespace ProductHub.Application.Products.UpdateProduct;

public sealed class UpdateProductService
{
    private readonly IProductRepository _productRepository;

    public UpdateProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException("Product not found.");

        product.Update(command.Name, command.Price, command.Quantity);

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}
