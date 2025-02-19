using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Products
{
    public interface IProductRepository 
        : IGenericRepository<Product, string>
    {
        IQueryable<Product> Products { get; }
    }
}
