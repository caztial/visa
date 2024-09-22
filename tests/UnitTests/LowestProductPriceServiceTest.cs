using Core.Entities;
using Core.Services;

namespace UnitTests;

public class LowestProductPriceServiceTest
{
    [Fact]
    public void GetItemTotal()
    {
        // Arrange
        var priceList = new PriceList();
        var productId = Guid.NewGuid();
        var rule1 = new QtyPriceRule(productId, "A", 4m);
        priceList.AddProductPriceRule(rule1);
        var service = new LowestProductPriceService();

        // Act
        var totalPrice = service.GetItemTotal(priceList, "A", 4);

        // Assert
        Assert.Equal(16m, totalPrice);
    }

    [Fact]
    public void GetItemTotalTwoRules()
    {
        // Arrange
        var priceList = new PriceList();
        var productId = Guid.NewGuid();
        var rule1 = new QtyPriceRule(productId, "A", 4m);
        var rule2 = new BundlePriceRule(productId, "A", 3, 10m, 4m);
        priceList.AddProductPriceRule(rule1);
        priceList.AddProductPriceRule(rule2);
        var service = new LowestProductPriceService();

        // Act
        var totalPrice = service.GetItemTotal(priceList, "A", 4);

        // Assert
        Assert.Equal(14m, totalPrice);
    }
}
