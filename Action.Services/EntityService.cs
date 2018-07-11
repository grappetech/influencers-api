using Action.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Action.Repository.Base;
using Action.Data.Models.Core.Watson;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Action.Services
{
    public class EntityService
    {

        private readonly IRepository<Entity> _repository;
        public EntityService(IRepository<Entity> repository)
        {
            _repository = repository;
        }


        public List<Entity> Get()
        {

            var list = this._repository.GetAll().Include(x => x.RelatedEntities).AsNoTracking().ToList();
            return list;

        }

        public Entity Get(long id)
        {
            var entity = this._repository.Find(id);
            return entity;
        }

        public async Task<long> Save(Entity entity)
        {
            if (entity.Id > 0)
            {
                this._repository.Update(entity);

            }
            else
            {
                this._repository.Add(entity);
            }

            long entityId = await this._repository.SaveChangesAsync();

            if (entityId > 0)
                entityId = entity.Id;

            return entityId;
        }
    }
}
