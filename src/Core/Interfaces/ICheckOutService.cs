namespace Core.Interfaces;

public interface ICheckOutService
{
    void NewCheckOut(Guid priceListId);
    void Scan(string sku, float quantity = 1);
    decimal GetTotal();
}
