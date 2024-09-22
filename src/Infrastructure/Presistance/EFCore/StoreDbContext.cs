using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Presistance.EFCore;

public class StoreDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<PriceList> PriceLists { get; set; }
    public DbSet<CheckOut> CheckOuts { get; set; }
    public string DbPath { get; }

    public StoreDbContext()
    {
        DbPath = System.IO.Path.Join("store.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");
}
