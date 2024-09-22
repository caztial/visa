using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductPriceService
{
    decimal GetItemTotal(PriceList priceList, string sku, int quantity);
}
