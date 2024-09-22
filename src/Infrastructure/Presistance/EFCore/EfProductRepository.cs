using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Presistance.EFCore;

public class EfProductRepository : IProductRepository
{
    private readonly StoreDbContext _context;

    public EfProductRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _context.Products.Where(x => x.Sku == sku).FirstOrDefaultAsync();
    }

    public async Task<Product> CreateAsync(Product entity)
    {
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
