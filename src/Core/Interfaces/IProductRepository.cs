using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetBySkuAsync(string sku);
    Task<Product> CreateAsync(Product entity);
}

public interface IPriceListRepository : IRepository<PriceList> { }

public interface ICheeseRepository : IRepository<CheckOut> { }
