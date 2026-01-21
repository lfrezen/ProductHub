using ProductHub.Application.Abstractions.Repositories;

namespace ProductHub.Application.Products.DeleteProduct;

public class DeleteProductService(IProductRepository repository)
{
    private readonly IProductRepository _repository = repository;

    public async Task ExecuteAsync(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException("Product not found.");

        await _repository.DeleteAsync(product, cancellationToken);
    }
}
