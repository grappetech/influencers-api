using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.Models.Watson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using Microsoft.EntityFrameworkCore;

namespace Action.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;

        public AccountController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        // GET: api/values
        [HttpGet("{id}")]
        public dynamic Get([FromRoute] int id)
        {
            try
            {
                if (_dbContext == null)
                    return NotFound("No database connection");
                var data = _dbContext.Accounts.Include(x=>x.Administrator).Include(x=>x.Plan).ThenInclude(x=>x.Features).Include(x=>x.Users).FirstOrDefault(x=>x.Id == id);
                return Ok(new
                {
                    data.Id,
                    data.Plan,
                    name = data.Administrator?.Name,
                    email = data.Administrator?.Email,
                    entities = new List<Entity>()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{id}/entities")]
        public dynamic Post([FromRoute] int id,[FromBody] List<AddEntityViewModel> model)
        {
            var account = _dbContext.Accounts.Find(id);
            foreach (var entity in model)
            {
                if (entity.Id == 0)
                {
                    var obj = new Entity
                    {
                        Name = entity.Entity,
                        CategoryId = Enum.Parse<ECategory>(entity.Type)
                    };
                    _dbContext.Entities.Add(obj);
                    account.Entities.Add(obj);

                }
                else
                {
                    if(account.Entities.All(x => x.Id != entity.Id))
                    account.Entities.Add(_dbContext.Entities.Find(entity.Id));
                }
            }
            _dbContext.Entry(account).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return account.Entities;
        }
    }

    public class AddEntityViewModel
    {
        public int Id { get; set; }
        public string Entity { get; set; }
        public string Type { get; set; }
    }
}