using Core.Entities;
using Core.Interfaces;
using Core.Services;
using NSubstitute;

namespace UnitTests;

public class CheckOutServiceTest
{
    private CheckOutService GetCheckOutService()
    {
        var priceList = new PriceList();
        var a = new Product("A");
        var b = new Product("B");
        var c = new Product("C");
        var d = new Product("D");

        priceList.AddProductPriceRule(new QtyPriceRule(a.Id, a.Sku, 50));
        priceList.AddProductPriceRule(new BundlePriceRule(a.Id, a.Sku, 3, 130, 50));
        priceList.AddProductPriceRule(new QtyPriceRule(b.Id, b.Sku, 30));
        priceList.AddProductPriceRule(new BundlePriceRule(b.Id, b.Sku, 2, 45, 30));
        priceList.AddProductPriceRule(new QtyPriceRule(c.Id, c.Sku, 20));
        priceList.AddProductPriceRule(new QtyPriceRule(d.Id, d.Sku, 15));

        IProductPriceService productPriceService = new LowestProductPriceService();

        IPriceListService priceListService = Substitute.For<IPriceListService>();
        priceListService.GetPriceList(Arg.Any<Guid>()).Returns(priceList);

        IProductService productService = Substitute.For<IProductService>();
        productService.GetProduct("A").Returns(a);
        productService.GetProduct("B").Returns(b);
        productService.GetProduct("C").Returns(c);
        productService.GetProduct("D").Returns(d);

        return new CheckOutService(productPriceService, priceListService, productService);
    }

    private decimal Price(string skuArray)
    {
        char[] skus = skuArray.ToCharArray();
        var checkOutService = GetCheckOutService();
        checkOutService.NewCheckOut(Guid.NewGuid());
        foreach (var sku in skus)
        {
            checkOutService.Scan(sku.ToString());
        }
        return checkOutService.GetTotal();
    }

    [Fact]
    public void TestTotals()
    {
        Assert.Equal(0, Price(""));
        Assert.Equal(50, Price("A"));
        Assert.Equal(80, Price("AB"));
        Assert.Equal(115, Price("CDBA"));

        Assert.Equal(100, Price("AA"));
        Assert.Equal(130, Price("AAA"));
        Assert.Equal(180, Price("AAAA"));
        Assert.Equal(230, Price("AAAAA"));
        Assert.Equal(260, Price("AAAAAA"));

        Assert.Equal(160, Price("AAAB"));
        Assert.Equal(175, Price("AAABB"));
        Assert.Equal(190, Price("AAABBD"));
        Assert.Equal(190, Price("DABABA"));
    }

    [Fact]
    public void TestIncremental()
    {
        var checkOutService = GetCheckOutService();
        checkOutService.NewCheckOut(Guid.NewGuid());
        Assert.Equal(0, checkOutService.GetTotal());

        checkOutService.Scan("A");
        Assert.Equal(50, checkOutService.GetTotal());

        checkOutService.Scan("B");
        Assert.Equal(80, checkOutService.GetTotal());

        checkOutService.Scan("A");
        Assert.Equal(130, checkOutService.GetTotal());

        checkOutService.Scan("A");
        Assert.Equal(160, checkOutService.GetTotal());

        checkOutService.Scan("B");
        Assert.Equal(175, checkOutService.GetTotal());
    }
}
