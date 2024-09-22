using System;
using Core.Common;
using Core.Interfaces;

namespace Core.Entities;

public class Product : BaseEntity, IAggregatedRoot
{
    public string Sku { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }

    // EF Core need this constructor
    protected Product() { }

    public Product(string sku)
    {
        Id = Guid.NewGuid();
        Sku = sku;
    }
}
