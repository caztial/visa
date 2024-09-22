using System;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class LowestProductPriceService : IProductPriceService
{
    public decimal GetItemTotal(PriceList priceList, string sku, int quantity)
    {
        var productPriceRules = priceList.GetProductPriceRules(sku);
        if (productPriceRules == null)
        {
            throw new InvalidOperationException("Product price rule not found");
        }
        decimal total = 0;

        foreach (var rule in productPriceRules)
        {
            // Get the lowest price using all the rules
            var price = rule.GetTotalPrice(quantity);
            if (total == 0 || price < total)
            {
                total = price;
            }
        }
        return total;
    }
}
