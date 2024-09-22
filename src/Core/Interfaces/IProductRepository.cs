using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository : IRepository<Product> { }

public interface IPriceListRepository : IRepository<PriceList> { }

public interface ICheeseRepository : IRepository<CheckOut> { }
