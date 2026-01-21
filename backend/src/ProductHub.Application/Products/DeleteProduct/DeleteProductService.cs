using ProductHub.Application.Abstractions.Repositories;

namespace ProductHub.Application.Products.DeleteProduct;

public class DeleteProductService
{
    private readonly IProductRepository _productRepository;

    public DeleteProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {command.Id} not found");

        await _productRepository.DeleteAsync(product, cancellationToken);
    }
}