using Action.Data.Models.Core;
using Action.Data.Models.Core.Watson;
using Action.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Services
{
    public class IndustryService
    {
        private readonly IRepository<Industry> _repository;
        public IndustryService(IRepository<Industry> repository)
        {
            _repository = repository;
        }

        public List<Industry> Get()
        {
            return this._repository.GetAll().ToList();
        }

        public Industry Get(int industryId)
        {
            return this._repository.QueryableEntity()
                        .Include(x => x.ScrapSources)
                        .ThenInclude(x=> x.ScrapSource)
                        .FirstOrDefault(i => i.Id == industryId);
        }

    }
}
