using Action.Data.Models.Core;
using Action.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Services
{
    public class EntityRoleService
    {
        private readonly IRepository<EntityRole> _repository;
        public EntityRoleService(IRepository<EntityRole> repository)
        {
            _repository = repository;
        }

        public List<EntityRole> GetEntityRoles()
        {
            return this._repository.GetAll().AsNoTracking().ToList();
        }

    }
}
