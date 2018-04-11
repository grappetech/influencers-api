using System;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Action.Controllers
{
    [Route("api/relationship-factors")]
    [EnableCors("Default")]
    [AllowAnonymous]
    public class RelationshipController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;
        
        public RelationshipController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }
        
        [HttpGet("")]
        public IActionResult Get([FromRoute] int id)
        {
            return ValidateUser(() =>
            {
                try
                {
                    if (_dbContext == null)
                        return NotFound("No database connection");
                    
                    var items = _dbContext.RelationTypes
                                          .AsNoTracking()
                                          .OrderBy(x => x.Name)
                                          .Select(cw=>new RelationTypeViewModel{
                                                Id = cw.Id,
                                                Name = cw.Name
                                           })
                                          .ToList();
                    return Ok(items);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            });
        }
    }
}