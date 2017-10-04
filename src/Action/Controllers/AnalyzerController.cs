﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    public class AnalyzerController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ApplicationDbContext _dbContext;

        public AnalyzerController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }
        
        

        // GET: api/values
        //[Authorize]
        [HttpGet("entities/{entity}")]
        public EntityList Get(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath,"App_Data","mock_entities.json"));
            var result = new EntityList();
            result.Entities = JsonConvert.DeserializeObject<List<SimpleEntity>>(json).Where(x=>x.Entity.ToLower().Contains(entity.ToLower())).ToList();
            return result;
        }
        
        
        //[Authorize]
        [HttpGet("personality/{entity}")]
        public dynamic GetPersonality(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath,"App_Data","mock_personality_result.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
            
        }
        
        
        //[Authorize]
        [HttpGet("data/{entity}")]
        public dynamic GetNlu(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath,"App_Data","mock_nlu_result.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
            
        }
        
        //[Authorize]
        [HttpGet("tone/{entity}")]
        public dynamic GetTone(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath,"App_Data","mock_tone_result.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
            
        }
        
        
        //[Authorize]
        [HttpPost("analyze")]
        public dynamic PostAnalyze(AnalyseRequest entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(new HostingEnvironment().ContentRootPath,"App_Data","mock_analyze_result.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
            
        }
    }
}
