using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashRegisterGoods.AllGoods;

namespace CashRegisterGoods.Data
{
    public class AppDbContext : DbContext 
    {
        public DbSet<Goods> Goods { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>()
                .Property(g => g.PDV)
                .HasColumnType("nvarchar(3)");

            modelBuilder.Entity<Goods>()
                .Property(g => g.Margin)
                .HasColumnType("nvarchar(3)");

            modelBuilder.Entity<Goods>()
                .Property(g => g.NetPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Goods>()
                .Property(g => g.Quantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Goods>()
                .Property(g => g.SellingPricePerUnit)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
