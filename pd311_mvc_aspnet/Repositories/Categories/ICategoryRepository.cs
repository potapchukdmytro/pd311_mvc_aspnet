using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories.Categories
{
    public interface ICategoryRepository 
        : IGenericRepository<Category, string>
    {
        IQueryable<Category> Categories { get; }
        Task<Category?> FindByNameAsync(string name);
    }
}
