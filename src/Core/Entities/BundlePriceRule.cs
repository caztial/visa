namespace Core.Entities;

public class BundlePriceRule : IBundledPriceRule
{
    public Guid ProductId { get; init; }
    public string ProductSku { get; init; }
    public decimal Price { get; init; }
    public string Type { get; init; }
    public int BundleSize { get; init; }
    public decimal UnitPrice { get; init; }

    public BundlePriceRule(
        Guid productId,
        string productSku,
        int bundleSize,
        decimal price,
        decimal unitPrice
    )
    {
        ProductId = productId;
        ProductSku = productSku;
        BundleSize = bundleSize;
        Price = price;
        UnitPrice = unitPrice;
        Type = nameof(BundlePriceRule);
    }

    public decimal GetTotalPrice(int qty)
    {
        if (qty % BundleSize != 0)
        {
            return Price * (qty / BundleSize) + (UnitPrice * (qty % BundleSize));
        }
        return Price * (qty / BundleSize);
    }
}
