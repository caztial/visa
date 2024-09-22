using Core.Entities;
using Core.Services;

namespace UnitTests;

public class QtyPriceRuleTest()
{
    [Fact]
    public void CreateQtyPriceRule()
    {
        // Arrange
        var productId = Guid.NewGuid();
        decimal price = 10m;

        // Act
        var rule = new QtyPriceRule(productId, "A", price);

        // Assert
        Assert.NotNull(rule);
        Assert.Equal(productId, rule.ProductId);
        Assert.Equal(price, rule.Price);
        Assert.Equal(nameof(QtyPriceRule), rule.Type);
    }

    [Fact]
    public void GetTotalPrice()
    {
        // Arrange
        var productId = Guid.NewGuid();
        decimal price = 10m;
        var rule = new QtyPriceRule(productId, "A", price);
        int qty = 5;

        // Act
        var totalPrice = rule.GetTotalPrice(qty);

        // Assert
        Assert.Equal(price * qty, totalPrice);
    }
}
