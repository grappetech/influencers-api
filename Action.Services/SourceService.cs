using Action.Data.Context;
using Action.Data.Models.Core.Scrap;
using Action.Repository.Base;
using Action.Repository.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action.Services
{
    public class SourceService
    {

        private readonly IRepository<ScrapSource> _repository;
        private readonly ApplicationDbContext _contexto;
        public SourceService(IRepository<ScrapSource> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _contexto = context;
        }

        public List<ScrapSource> Get()
        {
            return this._repository.GetAll().ToList();
        }

        public ScrapSource Get(int id)
        {
            //return this._repostiory.Find(id);
            var sourceScrap = this._repository
                                   .QueryableEntity()
                                   .Include(s => s.Industries)
                                  .FirstOrDefault(s => s.Id == id);

            return sourceScrap;

        }


        public ScrapSource GetDetailed(int id)
        {



        //public Industry Industry { get; set; }
        //public ScrapSource ScrapSource { get; set; }
        var sourceScrap = this._repository
                                   .QueryableEntity()
                                   .Include(s => s.Industries)
                                   .ThenInclude(s=>s.Industry)
                                   .ThenInclude(s=> s.Entities)                                   
                                  .FirstOrDefault(s => s.Id == id);

            return sourceScrap;

        }




        public async Task<int> Save(ScrapSource modelSourceScrap)
        {
            int sourceId = 0;

            if (modelSourceScrap.Id > 0)
            {
                //var sourceScrap = this._repository
                //                     .QueryableEntity()
                //                     .Include(s => s.Industries)
                //                     .FirstOrDefault(s => s.Id == modelSourceScrap.Id);

                var sourceScrap = _contexto.ScrapSources
                                  //.QueryableEntity()
                                  .Include(s => s.Industries)
                                  .FirstOrDefault(s => s.Id == modelSourceScrap.Id);


                if (sourceScrap != null)
                {


                    if (sourceScrap.Industries != null && sourceScrap.Industries.Count > 0 && modelSourceScrap.Industries != null && modelSourceScrap.Industries.Count > 0)
                    {
                        //remover todas as industrias e add novamente
                        var listIdIndustry = modelSourceScrap.Industries.Select(i => i.IndustryId).ToList();


                        if (sourceScrap.Industries != null)
                        {
                            var existingListIndustriId = sourceScrap.Industries.Select(i => i.IndustryId).ToList();

                            foreach (var industryId in existingListIndustriId)
                            {
                                // var remove = sourceScrap.Industries.FirstOrDefault(i => i.IndustryId == industryId);
                                // sourceScrap.Industries.Remove(remove);

                                var remove =_contexto.Set<ScrapSourceIndustry>().FirstOrDefault(s => s.IndustryId == industryId && s.ScrapSourceId == sourceScrap.Id);
                                _contexto.Set<ScrapSourceIndustry>().Remove(remove);
                            }

                            _contexto.SaveChanges();
                            //this._repository.Update(sourceScrap);
                        }


                    }


                    if (modelSourceScrap.Industries != null && modelSourceScrap.Industries.Count > 0)
                    {
                        var listIdIndustries = modelSourceScrap.Industries.Select(i => i.IndustryId).ToList();
                        listIdIndustries.ForEach(IndustryId =>
                        {
                            _contexto.Set<ScrapSourceIndustry>().Add(new ScrapSourceIndustry {
                                IndustryId = IndustryId,
                                ScrapSourceId = sourceScrap.Id
                            });

                            //sourceScrap.Industries.Add(new ScrapSourceIndustry
                            //{
                            //    IndustryId = IndustryId,
                            //    ScrapSourceId = sourceScrap.Id
                            //});
                        });

                        //_contexto.SaveChanges();
                    }



                    sourceScrap.StarTag = modelSourceScrap.StarTag;
                    sourceScrap.EndTag = modelSourceScrap.EndTag;
                    sourceScrap.Alias = modelSourceScrap.Alias;
                    sourceScrap.Dept = modelSourceScrap.Dept;
                    sourceScrap.Limit = modelSourceScrap.Limit;
                    sourceScrap.PageStatus = modelSourceScrap.PageStatus;
                    sourceScrap.Url = modelSourceScrap.Url;


                    //this._repository.Update(sourceScrap);
                     sourceId = await _contexto.SaveChangesAsync();
                }

            }
            else
            {
                this._repository.Add(modelSourceScrap);
                sourceId = await this._repository.SaveChangesAsync();
            }


            //int sourceId = await this._repository.SaveChangesAsync();
         

            if (sourceId > 0)
                sourceId = modelSourceScrap.Id;

            return sourceId;

        }
    }
}
