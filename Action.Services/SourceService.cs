using Action.Data.Models.Core.Scrap;
using Action.Repository.Base;
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
            return this._repostiory.Find(id);
        }

        public async Task<int> Save(ScrapSource sourceModel)
        {
            
            if(sourceModel.Id> 0)
            {
                this._repostiory.Update(sourceModel);
            }
            else
            {
                this._repostiory.Add(sourceModel);
            }


           int sourceId = await this._repostiory.SaveChangesAsync();

            if (sourceId > 0)
                sourceId = sourceModel.Id;

            return sourceId;

        }
    }
}
