using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Domain.Sales;
using ProductHub.Infrastructure.Data;

namespace ProductHub.Infrastructure.Repositories;

public class SaleRepository(ProductHubDbContext context) : ISaleRepository
{
    private readonly ProductHubDbContext _context = context;

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
