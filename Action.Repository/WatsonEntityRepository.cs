using Action.Data.Context;
using Action.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using WatsonEntity = Action.Data.Models.Watson.NLU.Entity;

namespace Action.Repository
{
    public class WatsonEntityRepository : Repository<WatsonEntity>
    {

        private readonly ApplicationDbContext _context;
        public WatsonEntityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Teste() { }
    }
}
