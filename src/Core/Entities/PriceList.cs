using System;
using Core.Common;
using Core.Interfaces;

namespace Core.Entities;

public class PriceList : BaseEntity, IAggregatedRoot
{
    readonly ICollection<IPriceRule> _productPriceRules = [];
    public IEnumerable<IPriceRule> ProductPriceRules => _productPriceRules;

    public PriceList()
    {
        Id = Guid.NewGuid();
    }

    public void AddProductPriceRule(IPriceRule productPrice)
    {
        _productPriceRules.Add(productPrice);
    }

    public void RemoveProductPriceRule(IPriceRule productPrice)
    {
        _productPriceRules.Remove(productPrice);
    }

    public IEnumerable<IPriceRule> GetProductPriceRules(Guid productId)
    {
        return _productPriceRules.Where(p => p.ProductId == productId);
    }

    public IEnumerable<IPriceRule> GetProductPriceRules(string sku)
    {
        return _productPriceRules.Where(p => p.ProductSku == sku);
    }
}
