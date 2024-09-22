using Core.Entities;

namespace UnitTests;

public class ProductTests
{
    [Fact]
    public void CreateProduct()
    {
        // Arrange
        var sku = "A";

        // Act
        var product = new Product(sku);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(sku, product.Sku);
        Assert.NotEqual(Guid.Empty, product.Id);
    }
}
