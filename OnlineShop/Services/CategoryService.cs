using OnlineShop.Data;
using OnlineShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Services
{
    public class CategoryService
    {
        private readonly AppDbContext context;

        public CategoryService(AppDbContext context)
        {
            this.context = context;
        }

        
        public async Task<List<Category>> GetListAsync()
        {
            return await context.Category.ToListAsync();
        }

        public async Task<Category> Create(Category category)
        {
            context.Add(category);
            await context.SaveChangesAsync();
            return category;
        }
        public async Task<Category?> Get(int id)
        {
            return await context.Category.Include(x => x.ProductList).SingleOrDefaultAsync(x => x.Id == id);

        }
        public async Task<Category> EditCategory(Category category)
        {
            context.Update(category);
            await context.SaveChangesAsync();
            return category;

        }
        public void RemoveCategory(Category category)
        {
            context.Remove(category);

        }



    }
}
