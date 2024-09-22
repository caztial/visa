using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class CheckOutService : ICheckOutService
{
    private readonly IProductPriceService _productPriceService;
    private readonly IPriceListService _priceListService;
    private readonly IProductService _productService;
    private CheckOut? checkOut;

    public CheckOutService(
        IProductPriceService productPriceService,
        IPriceListService priceListService,
        IProductService productService
    )
    {
        _productPriceService = productPriceService;
        _priceListService = priceListService;
        _productService = productService;
    }

    public void NewCheckOut(Guid priceListId)
    {
        var priceList = _priceListService.GetPriceList(priceListId);
        checkOut = new CheckOut(priceList);
    }

    public void Scan(string sku, float quantity = 1)
    {
        if (checkOut == null)
        {
            throw new InvalidOperationException("CheckOut not started");
        }
        var product = _productService.GetProduct(sku);
        checkOut.Scan(product, quantity);
        SetTotal();
    }

    public void SetTotal()
    {
        if (checkOut == null)
        {
            throw new InvalidOperationException("CheckOut not started");
        }
        var priceList = _priceListService.GetPriceList(checkOut.PriceListId);
        decimal total = 0;
        foreach (var item in checkOut.Items)
        {
            total += _productPriceService.GetItemTotal(priceList, item.Key, (int)item.Value);
        }
        checkOut.SetTotal(total);
    }

    public decimal GetTotal()
    {
        if (checkOut == null)
        {
            throw new InvalidOperationException("CheckOut not started");
        }
        return checkOut.Total;
    }
}
