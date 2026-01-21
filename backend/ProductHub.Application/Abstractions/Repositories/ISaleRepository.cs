using ProductHub.Domain.Sales;

namespace ProductHub.Application.Abstractions.Repositories;

public interface ISaleRepository
{
  Task AddAsync(Sale sale, CancellationToken cancellationToken);
}
