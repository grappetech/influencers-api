using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
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
        [Authorize]
        [HttpGet("entities/{{entity}}")]
        public EntityList Get(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(new HostingEnvironment().ContentRootPath,"App_Data","mock_entities.json"));
            var result = new EntityList();
            result.Entities = JsonConvert.DeserializeObject<List<SimpleEntity>>(json).Where(x=>x.Entity.ToLower().Contains(entity.ToLower())).ToList();
            return result;
        }
        
        
        [Authorize]
        [HttpGet("personality/{{entity}}")]
        public EntityList GetPersonality(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(new HostingEnvironment().ContentRootPath,"App_Data","mock_personality_result.json"));
            var result = new EntityList();
            result.Entities = JsonConvert.DeserializeObject<List<SimpleEntity>>(json).Where(x=>x.Entity.ToLower().Contains(entity.ToLower())).ToList();
            return result;
        }
        
        
        [Authorize]
        [HttpGet("tone/{{entity}}")]
        public EntityList GetTone(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(new HostingEnvironment().ContentRootPath,"App_Data","mock_tone_result.json"));
            var result = new EntityList();
            result.Entities = JsonConvert.DeserializeObject<List<SimpleEntity>>(json).Where(x=>x.Entity.ToLower().Contains(entity.ToLower())).ToList();
            return result;
        }
        
        [Authorize]
        [HttpGet("tone/{{entity}}")]
        public EntityList GetNlu(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(new HostingEnvironment().ContentRootPath,"App_Data","mock_nlu_result.json"));
            var result = new EntityList();
            result.Entities = JsonConvert.DeserializeObject<List<SimpleEntity>>(json).Where(x=>x.Entity.ToLower().Contains(entity.ToLower())).ToList();
            return result;
        }
        
        
        [Authorize]
        [HttpPost("analise")]
        public EntityList GetNlu(AnalyseRequest entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(new HostingEnvironment().ContentRootPath,"App_Data","mock_nlu_result.json"));
            var result = new EntityList();
            //result.Entities = JsonConvert.DeserializeObject<List<SimpleEntity>>(json).Where(x=>x.Entity.ToLower().Contains(entity.ToLower())).ToList();
            return result;
        }
    }
}
