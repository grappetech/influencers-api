using System;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.Models.Watson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Action.Controllers
{
    [Route("api/[controller]")]
    
    [EnableCors("Default")]
    [AllowAnonymous]
    public class EntityController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;

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
                    return NotFound("No database connection");
                var data = _dbContext.Entities.ToList();
                return data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public dynamic GetById(long id)
        {
            try
            {
                if (_dbContext == null)
                    return NotFound("No database connection");
                var data = _dbContext.Entities.FirstOrDefault(x => x.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public dynamic Post([FromBody] Entity entity)
        {
            try
            {
                if (_dbContext == null)
                {
                    return NotFound("No database connection");
                }
                var data = _dbContext.Entities.Add(entity);
                _dbContext.SaveChanges();
                return data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        // POST api/values
        [HttpPut]
        public dynamic Put([FromBody] Entity entity)
        {
            try
            {
                if (_dbContext == null)
                {
                    return NotFound("No database connection");
                }
                var data = _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}