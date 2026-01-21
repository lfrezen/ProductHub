using FluentAssertions;
using ProductHub.Domain.Products;

namespace ProductHub.Domain.Tests.Products;

public class ProductTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateProduct()
    {
        // Arrange
        var name = "Arroz";
        var price = 10.50m;
        var quantity = 100;

        // Act
        var product = new Product(name, price, quantity);

        // Assert
        product.Name.Should().Be(name);
        product.Price.Should().Be(price);
        product.Quantity.Should().Be(quantity);
        product.IsOutOfStock.Should().BeFalse();
        product.LastSaleDate.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithZeroQuantity_ShouldMarkAsOutOfStock()
    {
        // Arrange
        var name = "Feijão";
        var price = 8.00m;
        var quantity = 0;

        // Act
        var product = new Product(name, price, quantity);

        // Assert
        product.IsOutOfStock.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_WithInvalidName_ShouldThrowException(string invalidName)
    {
        // Arrange
        var price = 10.00m;
        var quantity = 10;

        // Act
        Action act = () => new Product(invalidName, price, quantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Product name is required.");
    }

    [Fact]
    public void Constructor_WithNegativePrice_ShouldThrowException()
    {
        // Arrange
        var name = "Macarrão";
        var price = -5.00m;
        var quantity = 10;

        // Act
        Action act = () => new Product(name, price, quantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Product price must be greater than zero.");
    }

    [Fact]
    public void Constructor_WithZeroPrice_ShouldThrowException()
    {
        // Arrange
        var name = "Açúcar";
        var price = 0m;
        var quantity = 10;

        // Act
        Action act = () => new Product(name, price, quantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Product price must be greater than zero.");
    }

    [Fact]
    public void Constructor_WithNegativeQuantity_ShouldThrowException()
    {
        // Arrange
        var name = "Café";
        var price = 15.00m;
        var quantity = -1;

        // Act
        Action act = () => new Product(name, price, quantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Product quantity cannot be negative.");
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateProduct()
    {
        // Arrange
        var product = new Product("Arroz", 10.00m, 50);
        var newName = "Arroz Integral";
        var newPrice = 12.00m;
        var newQuantity = 30;

        // Act
        product.Update(newName, newPrice, newQuantity);

        // Assert
        product.Name.Should().Be(newName);
        product.Price.Should().Be(newPrice);
        product.Quantity.Should().Be(newQuantity);
        product.IsOutOfStock.Should().BeFalse();
    }

    [Fact]
    public void Update_WithZeroQuantity_ShouldMarkAsOutOfStock()
    {
        // Arrange
        var product = new Product("Arroz", 10.00m, 50);

        // Act
        product.Update("Arroz", 10.00m, 0);

        // Assert
        product.IsOutOfStock.Should().BeTrue();
    }

    [Fact]
    public void RegisterSale_WithValidQuantity_ShouldDecreaseStock()
    {
        // Arrange
        var product = new Product("Arroz", 10.00m, 50);
        var quantitySold = 10;

        // Act
        product.RegisterSale(quantitySold);

        // Assert
        product.Quantity.Should().Be(40);
        product.LastSaleDate.Should().NotBeNull();
        product.LastSaleDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void RegisterSale_WhenStockReachesZero_ShouldMarkAsOutOfStock()
    {
        // Arrange
        var product = new Product("Feijão", 8.00m, 10);

        // Act
        product.RegisterSale(10);

        // Assert
        product.Quantity.Should().Be(0);
        product.IsOutOfStock.Should().BeTrue();
    }

    [Fact]
    public void RegisterSale_WithZeroQuantity_ShouldThrowException()
    {
        // Arrange
        var product = new Product("Arroz", 10.00m, 50);

        // Act
        Action act = () => product.RegisterSale(0);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Quantity sold must be greater than zero.");
    }

    [Fact]
    public void RegisterSale_WithNegativeQuantity_ShouldThrowException()
    {
        // Arrange
        var product = new Product("Arroz", 10.00m, 50);

        // Act
        Action act = () => product.RegisterSale(-5);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Quantity sold must be greater than zero.");
    }

    [Fact]
    public void RegisterSale_WithQuantityGreaterThanStock_ShouldThrowException()
    {
        // Arrange
        var product = new Product("Arroz", 10.00m, 10);

        // Act
        Action act = () => product.RegisterSale(15);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Insufficient product quantity.");
    }

    [Fact]
    public void MarkAsOutOfStock_ShouldSetIsOutOfStockToTrue()
    {
        // Arrange
        var product = new Product("Arroz", 10.00m, 50);

        // Act
        product.MarkAsOutOfStock();

        // Assert
        product.IsOutOfStock.Should().BeTrue();
    }
}