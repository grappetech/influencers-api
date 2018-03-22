using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Action.Models;
using System.Text.Encodings.Web;
using Action.VewModels;
using Microsoft.EntityFrameworkCore;

namespace Action.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    public class SecondaryPlansController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;

        public SecondaryPlansController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        [HttpGet("{accountId}")]
        public dynamic Get([FromRoute] int accountId)
        {
            try
            {
                if (_dbContext == null)
                    return NotFound("No database connection");
                var data = _dbContext.SecondaryPlans.ToList();
                var inuse = _dbContext.Accounts.Include(x => x.SecondaryPlans).FirstOrDefault(x => x.Id == accountId)
                    .SecondaryPlans.Select(x => x.Id).ToList();

                return Ok(data.Select(x => new
                {
                    x.Id,
                    x.AllowedUsers,
                    x.Name,
                    x.Price,
                    x.StartDate,
                    Included = inuse.Any(c => c == x.Id)
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}