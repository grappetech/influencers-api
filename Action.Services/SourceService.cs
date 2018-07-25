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

        private readonly IRepository<ScrapSource> _repostiory;
        public SourceService(IRepository<ScrapSource> repository)
        {
            _repostiory = repository;
        }

        public List<ScrapSource> Get()
        {
            return this._repostiory.GetAll().ToList();
        }

        public ScrapSource Get(int id)
        {
            //return this._repostiory.Find(id);
            var sourceScrap = this._repostiory
                                   .QueryableEntity()
                                   .Include(s => s.Industries)
                                  .FirstOrDefault(s => s.Id == id);

            return sourceScrap;

        }

     


        public async Task<int> Save(ScrapSource modelSourceScrap)
        {



            if (modelSourceScrap.Id > 0)
            {
                var sourceScrap = this._repostiory
                                     .QueryableEntity()
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
                                var remove = sourceScrap.Industries.FirstOrDefault(i => i.IndustryId == industryId);
                                sourceScrap.Industries.Remove(remove);
                            }

                            this._repostiory.Update(sourceScrap);
                        }


                    }


                    if (modelSourceScrap.Industries != null && modelSourceScrap.Industries.Count > 0)
                    {
                        modelSourceScrap.Industries.ForEach(industry =>
                        {
                            sourceScrap.Industries.Add(new ScrapSourceIndustry
                            {
                                IndustryId = industry.IndustryId,
                                ScrapSourceId = sourceScrap.Id
                            });
                        });
                    }



                    sourceScrap.StarTag = modelSourceScrap.StarTag;
                    sourceScrap.EndTag = modelSourceScrap.EndTag;
                    sourceScrap.Alias = modelSourceScrap.Alias;
                    sourceScrap.Dept = modelSourceScrap.Dept;
                    sourceScrap.Limit = modelSourceScrap.Limit;
                    sourceScrap.PageStatus = modelSourceScrap.PageStatus;
                    sourceScrap.Url = modelSourceScrap.Url;


                    this._repostiory.Update(sourceScrap);

                }

            }
            else
            {
                this._repostiory.Add(modelSourceScrap);
            }


            int sourceId = await this._repostiory.SaveChangesAsync();

            if (sourceId > 0)
                sourceId = modelSourceScrap.Id;

            return sourceId;

        }
    }
}
