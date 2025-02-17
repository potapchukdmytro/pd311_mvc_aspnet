using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Product model)
        {
            await _context.Products.AddAsync(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var model = await FindByIdAsync(id);
            if(model == null)
            {
                return false;
            }

            _context.Products.Remove(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return products;
        }

        public async Task<Product?> FindByIdAsync(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<bool> UpdateAsync(Product model)
        {
            _context.Products.Update(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
