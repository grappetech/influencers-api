using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EntityAnalyseController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ApplicationDbContext _dbContext;

        public EntityAnalyseController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        // GET: api/values
        [HttpGet("{{entity}}")]
        public IEnumerable<string> Get([FromQuery] string entity)
        {
            try
            {
                if (_dbContext == null)
                {
                    return new string[] {"No database connection"};
                }
                else
                {
                    var data = _dbContext.Visitors.Select(m => _htmlEncoder.Encode(m.Name)).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
