using Core.Entities;

namespace UnitTests;

public class BundlePriceRuleTest()
{
    [Fact]
    public void CreateBundlePriceRule()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int bundleSize = 3;
        decimal price = 10m;
        decimal unitPrice = 5m;

        // Act
        var rule = new BundlePriceRule(productId, "A", bundleSize, price, unitPrice);

        // Assert
        Assert.NotNull(rule);
        Assert.Equal(productId, rule.ProductId);
        Assert.Equal(bundleSize, rule.BundleSize);
        Assert.Equal(price, rule.Price);
        Assert.Equal(unitPrice, rule.UnitPrice);
        Assert.Equal(nameof(BundlePriceRule), rule.Type);
    }

    [Fact]
    public void GetTotalPrice()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int bundleSize = 3;
        decimal price = 10m;
        decimal unitPrice = 5m;
        var rule = new BundlePriceRule(productId, "A", bundleSize, price, unitPrice);
        int qty = 5;

        // Act
        var totalPrice = rule.GetTotalPrice(qty);

        // Assert
        Assert.Equal(price * (qty / bundleSize) + (unitPrice * (qty % bundleSize)), totalPrice);
    }
}
