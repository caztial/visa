using System;
using Core.Entities;
using Infrastructure.Presistance.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTests.Presistance;

public class EfProductRepositoryTest
{
    [Fact]
    public async Task CreateAsyncTest()
    {
        // Arrange
        using (var context = new StoreDbContext())
        {
            context.Database.EnsureCreated();
            var repository = new EfProductRepository(context);
            var product = new Product("A")
            {
                Name = "Product Name",
                Description = "Product Description"
            };

            // Act
            var result = await repository.CreateAsync(product);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Sku, result.Sku);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
        }
    }
}

public class EfPriceListRepositoryTest
{
    readonly StoreDbContext _storeDbContext;

    public EfPriceListRepositoryTest()
    {
        _storeDbContext = new StoreDbContext();
        _storeDbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task CreatePriceListTest()
    {
        var repository = new EfPriceListRepository(_storeDbContext);
        var priceList = new PriceList();

        var result = await repository.CreateAsync(priceList);

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task CreatePriceListWithMultipleProductPriceRulesTest()
    {
        var repository = new EfPriceListRepository(_storeDbContext);
        var priceList = new PriceList();
        priceList.AddProductPriceRule(new QtyPriceRule(Guid.NewGuid(), "A", 14));
        priceList.AddProductPriceRule(new BundlePriceRule(Guid.NewGuid(), "A", 3, 10, 4));

        var result = await repository.CreateAsync(priceList);
        Assert.NotNull(result);
        Assert.Equal(2, result.ProductPriceRules.Count(a => a.ProductSku.Equals("A")));
    }
}
