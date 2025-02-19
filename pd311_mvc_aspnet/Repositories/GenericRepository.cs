﻿using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Repositories
{
    public class GenericRepository<TModel, TId>
        : IGenericRepository<TModel, TId>
        where TModel : class, IBaseModel<TId>
        where TId : notnull
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(TModel model)
        {
            await _context.Set<TModel>().AddAsync(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            var model = await FindByIdAsync(id);
            if (model == null)
            {
                return false;
            }

            _context.Set<TModel>().Remove(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<TModel?> FindByIdAsync(TId id)
        {
            var product = await _context
                .Set<TModel>()
                .FirstOrDefaultAsync(p => p.Id != null ? p.Id.Equals(id) : default);
            return product;
        }

        public IQueryable<TModel> GetAll()
        {
            return _context.Set<TModel>();
        }

        public async Task<bool> UpdateAsync(TModel model)
        {
            _context.Set<TModel>().Update(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
