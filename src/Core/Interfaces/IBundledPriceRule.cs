namespace Core.Entities;

public interface IBundledPriceRule : IPriceRule
{
    int BundleSize { get; init; }
    decimal UnitPrice { get; init; }
}
