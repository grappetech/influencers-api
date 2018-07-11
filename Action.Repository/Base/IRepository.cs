using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);        
        TEntity Find(params object[] key);
        void Update(TEntity entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Add(TEntity entity);
        void Remove(Func<TEntity, bool> predicate);
    }
}
