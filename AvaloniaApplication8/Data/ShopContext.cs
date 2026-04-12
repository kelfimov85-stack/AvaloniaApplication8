using System;
using AvaloniaApplication8;
using AvaloniaApplication8.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication8.Data
{
    public class ShopContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=lesson");
        }
    }
}
