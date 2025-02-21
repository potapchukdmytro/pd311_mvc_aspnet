using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Categories
{
    public class CategoryRepository
        : GenericRepository<Category, string>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
            : base(context) 
        {
            _context = context;
        }

        public IQueryable<Category> Categories => GetAll().Include(c => c.Products);

        public async Task<Category?> FindByNameAsync(string name)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == null 
                ? false 
                : c.Name.ToLower() == name.ToLower());
            return category;
        }
    }
}
