using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
    {
        Task<TEntity> GetEntity(TKey id);
        Task<IEnumerable<TEntity>> GetAll();
        Task AddEntities(TEntity entity);
        void UpdateEntities(TEntity entity);
        void RemoveEntities(TKey Id);
        Task SaveChanges();

    }
}
