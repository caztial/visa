using System;
using Core.Entities;
using Infrastructure.Presistance.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTests.Presistance;

public class EfProductRepositoryTest
{
    [Fact]
    public async Task CreateAsync()
    {
        // Arrange
        using (var context = new StoreDbContext())
        {
            context.Database.EnsureDeleted();
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
