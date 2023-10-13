using System.Collections.Generic;
using OnlineShop.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
