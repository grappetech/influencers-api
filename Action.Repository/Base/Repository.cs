
using Action.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Repository.Base
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {

        private readonly ApplicationDbContext _context;


        public Repository(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }


        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public TEntity Find(params object[] key)
        {
            return _context.Set<TEntity>().Find(key);

        }

        public IQueryable<TEntity> QueryableEntity()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            var entity = _context.Set<TEntity>().Where(predicate).AsQueryable();
            return entity;
        }



        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
            
        }

      

        public void Remove(Func<TEntity, bool> predicate)
        {
            _context.Set<TEntity>()
                         .Where(predicate)
                         .ToList()
                         .ForEach(entityDel => _context.Set<TEntity>().Remove(entityDel));
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        public void Update(TEntity entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
        }
    }
}
