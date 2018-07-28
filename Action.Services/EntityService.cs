using Action.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Action.Repository.Base;
using Action.Data.Models.Core.Watson;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Action.Data.Models.Core.Scrap;
using Action.Data.Context;

namespace Action.Services
{
    public class EntityService
    {
        private readonly ApplicationDbContext _contexto;
        private readonly IRepository<Entity> _repository;
        private readonly IRepository<ScrapSourceEntity> _scrapSourceEntityRepository;
        public EntityService(IRepository<Entity> repository, IRepository<ScrapSourceEntity> scrapSourceEntityRepository, ApplicationDbContext contexto)
        {
            _repository = repository;
            _scrapSourceEntityRepository = scrapSourceEntityRepository;
            _contexto = contexto;
        }


        public List<Entity> Get()
        {

            var list = this._repository.GetAll().Include(x => x.RelatedEntities).AsNoTracking().ToList();
            return list;

        }

        public Entity Get(long id)
        {
            //var entity = this._repository.Find(id);
            var entity = this._repository.QueryableEntity()
                                        .Include(x => x.ScrapSources)
                                        .ThenInclude(x => x.ScrapSource)
                                        .FirstOrDefault(e => e.Id == id);
            return entity;
        }

        public async Task<long> Save(Entity modelEntity)
        {
            long entityId = 0;
            if (modelEntity.Id > 0)
            {

                //       
                //var entity = this._repository
                //                     .QueryableEntity()
                //                      .AsNoTracking()
                //                     .Include(s => s.ScrapSources)                                    
                //                     .FirstOrDefault(s => s.Id == modelEntity.Id);


                var entity =  _contexto.Entities.Include(s => s.ScrapSources).FirstOrDefault(s => s.Id == modelEntity.Id);


                if (entity != null)
                {


                    //buscar entities sources da entidade
               


                    if (entity.ScrapSources != null && entity.ScrapSources.Count > 0 && modelEntity.ScrapSources != null && modelEntity.ScrapSources.Count > 0)
                   

                    {
                        //remover todas as industrias e add novamente
                        ////var listScrapSourceId = modelEntity.ScrapSources.Select(i => i.ScrapSourceId).ToList();
                        //var listScrapSourceId = entity.ScrapSources.Select(i => i.ScrapSourceId).ToList();


                        if (entity.ScrapSources != null)
                        {
                            var existingScrapSourceiId = entity.ScrapSources.Select(i => i.ScrapSourceId).ToList();

                            foreach (var scrapSourceId in existingScrapSourceiId)
                            {
                                //var scrapSourceToRemove = entity.ScrapSources.FirstOrDefault(i => i.ScrapSourceId == scrapSourceId);
                                //entity.ScrapSources.Remove(scrapSourceToRemove);

                                var remove = _contexto.Set<ScrapSourceEntity>().FirstOrDefault(s => s.EntityId == entity.Id && s.ScrapSourceId == scrapSourceId);
                                _contexto.Set<ScrapSourceEntity>().Remove(remove);



                            }
                            //this._scrapSourceEntityRepository.SaveChanges();
                            //this._repository.Update(entity);
                            //this._repository.SaveChanges();

                            _contexto.SaveChanges();
                        }


                    }


                    if (modelEntity.ScrapSources != null && modelEntity.ScrapSources.Count > 0)
                    {
                        modelEntity.ScrapSources.ForEach(scrapSource =>
                        {
                            //entity.ScrapSources.Add(new Data.Models.Core.Scrap.ScrapSourceEntity
                            //{ 
                            //    ScrapSourceId = scrapSource.ScrapSourceId
                            //});

                            _contexto.Set<ScrapSourceEntity>().Add(new ScrapSourceEntity {
                                EntityId = entity.Id,
                                ScrapSourceId = scrapSource.ScrapSourceId
                            });


                        });

                       
                    }




                    entity.IndustryId = modelEntity.IndustryId;
                    entity.Name = modelEntity.Name;
                    entity.Alias = modelEntity.Alias;
                    entity.SiteUrl = modelEntity.SiteUrl;
                    entity.PictureUrl = modelEntity.PictureUrl;
                    entity.FacebookUser = modelEntity.FacebookUser;
                    entity.TweeterUser = modelEntity.TweeterUser;
                    entity.CategoryId = modelEntity.CategoryId;
                    entity.IndustryId = modelEntity.IndustryId;
                    entity.ExecutionInterval = modelEntity.ExecutionInterval;
                    entity.Tier = modelEntity.Tier;


                    //this._repository.Update(entity);
                  entityId=  _contexto.SaveChanges();

                }
            }
            else
            {
                this._repository.Add(modelEntity);
                entityId = await this._repository.SaveChangesAsync();
            }

          

            //try
            //{
            //    entityId = await this._repository.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}


            if (entityId > 0)
                entityId = modelEntity.Id;

            return entityId;
        }
    }
}
