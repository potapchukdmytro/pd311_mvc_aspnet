using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories
{
    public interface IGenericRepository<TModel, TId>
        where TModel : class, IBaseModel<TId>
        where TId : notnull
    {
        Task<bool> CreateAsync(TModel model);
        Task<bool> UpdateAsync(TModel model);
        Task<bool> DeleteAsync(TId id);
        IQueryable<TModel> GetAll();
        Task<TModel?> FindByIdAsync(TId id);
    }
}
