using Core.Entities;

namespace Core.Interfaces;

public interface IPriceListService
{
    PriceList CreatePriceList(string name);
    PriceList GetPriceList(Guid priceListId);
    void AddProductPrice(Guid priceListId, IPriceRule productPrice);
}
