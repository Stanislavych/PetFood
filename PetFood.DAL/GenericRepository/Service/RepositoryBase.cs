using Microsoft.EntityFrameworkCore;
using PetFood.DAL.GenericRepository.Interface;
using System.Linq.Expressions;

namespace PetFood.DAL.GenericRepository.Service
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PetFood.DAL.DatabaseContext.DatabaseContext _context;

        public RepositoryBase(PetFood.DAL.DatabaseContext.DatabaseContext context)
        {
            _context = context;
        }

        public DbSet<T> Set<T>()
            where T : class
        {
            return _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await Task.Run(() => Set<T>().AddAsync(entity));
        }

        
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            //а тебе пря во всех случаях нужно AsNoTracking?
            return await Task.Run(() => Set<T>().AsNoTracking());
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            //а тебе пря во всех случаях нужно AsNoTracking?
            return await Task.Run(() => Set<T>().Where(expression).AsNoTracking());
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => Set<T>().Remove(entity));
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => Set<T>().Update(entity));
        }
    }
}
