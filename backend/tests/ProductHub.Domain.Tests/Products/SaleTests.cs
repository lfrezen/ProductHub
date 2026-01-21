using FluentAssertions;
using ProductHub.Domain.Sales;

namespace ProductHub.Domain.Tests.Sales;

public class SaleTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateSale()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = 10;

        // Act
        var sale = new Sale(productId, quantity);

        // Assert
        sale.ProductId.Should().Be(productId);
        sale.Quantity.Should().Be(quantity);
        sale.SaleDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Constructor_WithEmptyProductId_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.Empty;
        var quantity = 10;

        // Act
        Action act = () => new Sale(productId, quantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("ProductId is required.");
    }

    [Fact]
    public void Constructor_WithZeroQuantity_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = 0;

        // Act
        Action act = () => new Sale(productId, quantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Sale quantity must be greater than zero.");
    }

    [Fact]
    public void Constructor_WithNegativeQuantity_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = -5;

        // Act
        Action act = () => new Sale(productId, quantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Sale quantity must be greater than zero.");
    }
}