using ProductHub.Domain.Common;

namespace ProductHub.Domain.Products;

public class Product : BaseEntity
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public bool IsOutOfStock { get; private set; }
    public DateTime? LastSaleDate { get; private set; }

    private Product() { }

    public Product(string name, decimal price, int quantity)
    {
        Validate(name, price, quantity);

        Name = name;
        Price = price;
        Quantity = quantity;
        IsOutOfStock = quantity <= 0;
    }

    public void RegisterSale(int quantitySold)
    {
        if (quantitySold <= 0)
            throw new InvalidOperationException("Quantity sold must be greater than zero.");

        if (quantitySold > Quantity)
            throw new InvalidOperationException("Insufficient product quantity.");

        Quantity -= quantitySold;
        LastSaleDate = DateTime.UtcNow;

        if (Quantity == 0)
            IsOutOfStock = true;
    }

    public void MarkAsOutOfStock()
    {
        IsOutOfStock = true;
    }

    private static void Validate(string name, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (price <= 0)
            throw new ArgumentException("Product price must be greater than zero.");

        if (quantity < 0)
            throw new ArgumentException("Product quantity cannot be negative.");
    }
}
