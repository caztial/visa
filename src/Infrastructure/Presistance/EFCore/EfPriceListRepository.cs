using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Presistance.EFCore;

public class EfPriceListRepository : IPriceListRepository
{
    private readonly StoreDbContext _context;

    public EfPriceListRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<PriceList> CreateAsync(PriceList entity)
    {
        _context.PriceLists.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
