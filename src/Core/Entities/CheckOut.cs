using System;
using Core.Common;
using Core.Interfaces;

namespace Core.Entities;

public class CheckOut : BaseEntity, IAggregatedRoot
{
    public Guid PriceListId { get; init; }
    public IDictionary<string, float> Items { get; } = new Dictionary<string, float>();
    public decimal Total { get; private set; }

    // EF Core need this constructor
    protected CheckOut() { }

    public CheckOut(PriceList priceList)
    {
        Id = Guid.NewGuid();
        PriceListId = priceList.Id;
    }

    public void SetTotal(decimal total)
    {
        Total = total;
    }

    public void Scan(Product product, float quantity = 1)
    {
        if (!Items.ContainsKey(product.Sku))
        {
            Items.Add(product.Sku, quantity);
        }
        else
        {
            Items[product.Sku] += quantity;
        }
    }

    public void RemoveItem(Product product)
    {
        if (Items.ContainsKey(product.Sku))
        {
            if (Items[product.Sku] > 1)
            {
                Items[product.Sku]--;
            }
            else
            {
                Items.Remove(product.Sku);
            }
        }
    }
}
