using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Action.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Action.Controllers
{
    [Route("api/location")]
    [EnableCors("Default")]
    public class LocationController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        public LocationController(ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
        }

        [Route("cities")]
        public IActionResult GetCities([FromQuery] string name)
        {
            
           // var result = new List<string>();

            var query = _dbContext.Cities.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()))
                .AsNoTracking()
                .Include(x => x.State).Select(c => new
                {
                    c.Id,
                    c.Name,
                    State = new
                    {
                        c.State.Id,
                        c.State.Code,
                        c.State.Name
                    }
                });

            return Ok(query.ToList());
        }
        
        [Route("states")]
        public IActionResult GetStates([FromQuery] string name)
        {
            
            // var result = new List<string>();

            var query = _dbContext.States.Where(z => z.Name.ToLower().Contains(name.ToLower().Trim()) || z.Code.ToLower().Equals(name.ToLower()))
                .AsNoTracking()
                .Select(x=>new
                {
                    x.Id,
                    x.Code,
                    x.Name
                });

            return Ok(query.ToList());
        }
        
    }
}