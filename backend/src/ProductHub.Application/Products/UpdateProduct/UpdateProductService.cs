using ProductHub.Application.Abstractions.Repositories;

namespace ProductHub.Application.Products.UpdateProduct;

public class UpdateProductService
{
    private readonly IProductRepository _productRepository;

    public UpdateProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {command.Id} not found");

        product.Update(command.Name, command.Price, command.Quantity);

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}