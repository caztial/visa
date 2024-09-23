using Core.Common;

namespace Core.Entities;

public class QtyPriceRule : BaseEntity, IPriceRule
{
    public Guid ProductId { get; init; }
    public string ProductSku { get; init; }
    public decimal Price { get; init; }
    public string Type { get; init; }

    public QtyPriceRule(Guid productId, string productSku, decimal price)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductSku = productSku;
        Price = price;
        Type = nameof(QtyPriceRule);
    }

    public decimal GetTotalPrice(int qty)
    {
        return Price * qty;
    }
}
