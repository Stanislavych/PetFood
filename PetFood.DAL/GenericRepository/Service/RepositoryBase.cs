using Microsoft.EntityFrameworkCore;
using PetFood.DAL.GenericRepository.Interface;
using System.Linq.Expressions;

namespace PetFood.DAL.GenericRepository.Service
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PetFood.DAL.DatabaseContext.DatabaseContext DatabaseContext;

        public RepositoryBase(PetFood.DAL.DatabaseContext.DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task CreateAsync(T entity)
        {
            await Task.Run(() => DatabaseContext.Set<T>().AddAsync(entity));
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await Task.Run(() => DatabaseContext.Set<T>().AsNoTracking());
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => DatabaseContext.Set<T>().Where(expression).AsNoTracking());
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => DatabaseContext.Set<T>().Remove(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => DatabaseContext.Set<T>().Update(entity));
        }
    }
}
