using BasketService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasketService.Infrastructure
{
    public class BasketDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {

        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
    }
}
