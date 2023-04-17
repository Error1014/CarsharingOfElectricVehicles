using Infrastructure.HelperModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddEntities(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void UpdateEntities(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
        public void RemoveEntities(TKey Id)
        {
            var entity = context.Set<TEntity>().Find(Id);
            context.Set<TEntity>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
