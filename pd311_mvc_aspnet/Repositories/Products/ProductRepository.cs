using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Products
{
    public class ProductRepository
        : GenericRepository<Product, string>, IProductRepository
    {
        public ProductRepository(AppDbContext context)
            : base(context)
        { }

        public IQueryable<Product> Products => GetAll().Include(p => p.Category);

        public IQueryable<Product> GetByCategory(string categoryName)
        {
            return Products
                .Where(p => p.Category == null
                ? false
                : p.Category.Name == null ? false 
                : p.Category.Name.ToLower() == categoryName.ToLower());
        }
    }
}
