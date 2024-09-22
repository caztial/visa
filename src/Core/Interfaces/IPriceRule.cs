namespace Core.Entities;

public interface IPriceRule
{
    Guid ProductId { get; init; }
    public string ProductSku { get; init; }
    decimal Price { get; init; }
    string Type { get; init; }
    decimal GetTotalPrice(int qty);
}
