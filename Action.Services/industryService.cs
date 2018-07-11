using Action.Data.Models.Core;
using Action.Repository.Base;
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

    }
}
