using ProductHub.Application.Products.CreateProduct;
using ProductHub.Application.Products.RegisterSale;

namespace ProductHub.Application.Abstractions.Services;

public interface IProductService
{
  Task<Guid> CreateAsync(CreateProductCommand command, CancellationToken cancellationToken);
  Task RegisterSaleAsync(RegisterSaleCommand command, CancellationToken cancellationToken);
}
