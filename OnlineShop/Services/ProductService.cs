using OnlineShop.Data;
using OnlineShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Services
{
    public class ProductService
    {
        private readonly AppDbContext context;

        public ProductService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Product> GetAsync(int id)
        {
            return await context.Product.SingleOrDefaultAsync(e => e.Id == id);
        }
        public async Task<List<Product>> GetListAsync()
        {
            return await context.Product.Include(e => e.Category).ToListAsync();
        }
        public async Task<Product?> Get(int id)
        {
            return await context.Product.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<Product> Create(Product Product)
        {
            context.Add(Product);
            await context.SaveChangesAsync();
            return Product;
        }
        public async Task<Product> EditProduct (Product product)
        {
            context.Update(product);
            await context.SaveChangesAsync();
            return product;

        }
        public void RemoveProduct(Product product)
        {
            context.Remove(product);
        }

    }
}
