using Action.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatsonEntity = Action.Data.Models.Watson.NLU.Entity;

namespace Action.Services
{
    public class WatsonEntityService
    {

        private readonly IRepository<WatsonEntity> _repostiory;
        public WatsonEntityService(IRepository<WatsonEntity> repository)
        {
            _repostiory = repository;
        }

        /// <summary>
        /// Retorna lista de entidades core (wattson) que não tenha vinculo
        /// </summary>
        /// <returns></returns>
        public List<WatsonEntity> GetNotRelatedCoreEntities(int takeOnly = 0)
        {
            var list = this._repostiory.Get(e => e.EntityId == null);

            if (takeOnly > 0)
            {
                list = list.Take(takeOnly);
            }

            return list.ToList();
        }

        public List<WatsonEntity> GetRelatedCoreEntities(long entityId)
        {
            var list = this._repostiory.Get(e => e.EntityId == entityId);
            return list.ToList();
        }

        
        public async Task RemoveWatsonEntity(string[] arrEntityToRelate, long entityId)
        {
            var listWatsonEntity = this._repostiory.Get(we => arrEntityToRelate.Any(id => id == we.Id.ToString())).ToList();
            try
            {
                foreach (var watsonEntity in listWatsonEntity)
                {
                    watsonEntity.EntityId = null;
                    this._repostiory.Update(watsonEntity);
                }

                if (listWatsonEntity != null && listWatsonEntity.Count > 0)
                {
                    await this._repostiory.SaveChangesAsync();
                }
            }
            catch
            {

            }
        }

        public async Task AddWatsonEntity(string[] arrUnrelatedWatsonEntityChecked, long entityId)
        {
            var listWatsonEntity = this._repostiory.Get(we => arrUnrelatedWatsonEntityChecked.Any(id => id == we.Id.ToString())).ToList();
            try
            {
                foreach (var watsonEntity in listWatsonEntity)
                {
                    watsonEntity.EntityId = entityId;
                    this._repostiory.Update(watsonEntity);
                }

                if (listWatsonEntity != null && listWatsonEntity.Count > 0)
                {
                    await this._repostiory.SaveChangesAsync();
                }
            }
            catch
            {

            }
        }
    }
}
