using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Categories
{
    public class CategoryRepository
        : GenericRepository<Category, string>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context) { }

        public IQueryable<Category> Categories => GetAll().Include(c => c.Products);
    }
}
