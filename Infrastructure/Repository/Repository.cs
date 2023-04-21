using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
    {
        protected DbContext context;
        protected IQueryable<TEntity> Set => context.Set<TEntity>().AsQueryable();
        public Repository(DbContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> GetEntity(TKey id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetPage(PageFilter pageFilter)
        {
            var query = context.Set<TEntity>().AsQueryable();
            query = query
                .OrderBy(x => x.Id)
                .Skip((pageFilter.NumPage - 1) * pageFilter.SizePage)
                .Take(pageFilter.SizePage);
            return await query.ToListAsync();
        }
        public async Task AddEntities(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void UpdateEntities(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
        public void RemoveEntities(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}
