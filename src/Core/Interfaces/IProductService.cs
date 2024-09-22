using Core.Entities;

namespace Core.Interfaces;

public interface IProductService
{
    Product CreateProduct(string sku);
    Product GetProduct(Guid productId);
    Product GetProduct(string sku);
}
