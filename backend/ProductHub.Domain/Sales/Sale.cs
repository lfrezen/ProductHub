using ProductHub.Domain.Common;
using ProductHub.Domain.Products;

namespace ProductHub.Domain.Sales;

public class Sale : BaseEntity
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public int Quantity { get; private set; }
    public DateTime SaleDate { get; private set; }

    private Sale() { }

    public Sale(Guid productId, int quantity)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId is required.");

        if (quantity <= 0)
            throw new ArgumentException("Sale quantity must be greater than zero.");

        ProductId = productId;
        Quantity = quantity;
        SaleDate = DateTime.UtcNow;
    }
}
