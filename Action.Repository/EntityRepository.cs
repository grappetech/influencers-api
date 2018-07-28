using Action.Data.Context;
using Action.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Action.Repository
{
    public class EntityRepository : Repository<Action.Data.Models.Core.Watson.Entity>
    {

        private readonly ApplicationDbContext _context;
        public EntityRepository(ApplicationDbContext context):base(context)
        {

        }

      
    }
}
