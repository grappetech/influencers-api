﻿using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.VewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    public class AnalyzerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly Random number = new Random(DateTime.Now.Millisecond);

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
            var result = new EntityList();

            var entities = _dbContext.Entities
                .Where(x => x.Alias.ToLower().Contains(entity.ToLower()) && x.CategoryId == ECategory.Brand).ToList();

            foreach (var item in entities)
                result.Entities.Add(new SimpleEntity
                {
                    Entity = item.Name,
                    Id = item.Id,
                    Type = item.Category
                });

            return result;
        }


        // GET: api/values
        //[Authorize]
        [HttpGet("persons/{entity}")]
        public EntityList GetPersons(string entity)
        {
            var result = new EntityList();

            var entities = _dbContext.Entities.Where(x =>
                x.Alias.ToLower().Contains(entity.ToLower()) &&
                (x.CategoryId == ECategory.Personality || x.CategoryId == ECategory.Person)).ToList();

            foreach (var item in entities)
                result.Entities.Add(new SimpleEntity
                {
                    Entity = item.Name,
                    Id = item.Id,
                    Type = item.Category
                });

            return result;
        }

        //[Authorize]
        [HttpGet("personality/{entity}")]
        public dynamic GetPersonality(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data",
                "mock_personality_result.json"));
            var result = JsonConvert.DeserializeObject<dynamic>(json);
            result.mentions = number.Next(100, 300);
            result.sources = number.Next(1, 10);
            return result;
        }


        //[Authorize]
        [HttpGet("data/{entity}")]
        public dynamic GetNlu(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data", "mock_nlu_result.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("tone/{entity}")]
        public dynamic GetTone(string entity)
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data", "mock_tone_result.json"));
            var result = JsonConvert.DeserializeObject<dynamic>(json);
            var number2 = new Random(DateTime.Now.Millisecond);
            var number3 = number2.Next(100, 999) / 1000.0;
            result.document_tone.positive = number3;
            result.document_tone.negative = 1.0 - number3;
            return result;
        }


        //[Authorize]
        [HttpPost("analyze")]
        public dynamic PostAnalyze(AnalyseRequest entity)
        {
            if (entity.Brand == "" || entity.Briefing == "" || entity.Factor == "" || entity.Product == "")
                return BadRequest("Dados inválidos");

            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_analyze_result.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }
    }
}