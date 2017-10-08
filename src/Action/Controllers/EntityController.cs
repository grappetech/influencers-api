using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Microsoft.AspNetCore.Mvc;

using Action.Models.Watson;

namespace Action.Controllers
{
    [Route("api/[controller]")]
    public class EntityController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ApplicationDbContext _dbContext;

        public EntityController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        // GET: api/values
        [HttpGet]
        public dynamic Get()
        {
            try
            {
                if (_dbContext == null)
                {
                    return NotFound("No database connection");
                }
                else
                {
                    var data = _dbContext.Entities.ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public dynamic Post([FromBody]Entity entity)
        {
			try
			{
				if (_dbContext == null)
				{
					return NotFound("No database connection");
				}
				else
				{
					var data = _dbContext.Entities.Add(entity);
                    _dbContext.SaveChanges();
					return data;
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
        }
    }
}
