using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using Action.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
            return ValidateUser(() => { return Ok(); });
        }
    }
}