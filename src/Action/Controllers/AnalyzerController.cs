using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using Action.Models;
using Action.Services.Watson.PersonalityInsights;
using Action.VewModels;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

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
        public dynamic Get(string entity)
        {
            try
            {
                var result = new EntityList();

                var ids = _dbContext.Database.GetDbConnection()
                    .ExecuteReader("SELECT DISTINCT EntityId AS Id FROM Personalities;")
                    .Parse<IdListViewModel<long>>().ToList();

                var entities = _dbContext.Entities
                    .Where(x => x.Alias.ToLower().Contains(entity.ToLower()) &&
                                x.CategoryId == ECategory.Brand).ToList();

                foreach (var item in entities)
                    if (ids.Any(x => x.Id == item.Id))
                        result.Entities.Add(new SimpleEntity
                        {
                            Entity = item.Name,
                            Id = item.Id,
                            Type = item.Category
                        });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public dynamic GetPersonality(long entity)
        {
            try
            {
                var mentions = _dbContext.EntityMentions.Count(x => x.EntityId == entity);
                var sources = _dbContext.EntityMentions.Select(x => x.ScrapedPageId).Distinct().Count();
                var personalities = _dbContext.Personalities
                    .Include(x => x.Personality)
                    .ThenInclude(x => x.Details)
                    .Include(x => x.Needs)
                    .Include(x => x.Values).Where(x => x.EntityId == entity).ToList();

                var needs = personalities.SelectMany(x => x.Needs.Select(c => new {c.Name, c.Percentile}))
                    .GroupBy(x => x.Name).Select(c => new {name = c.Key, percentile = c.Average(p => p.Percentile)});

                var personality = personalities
                    .SelectMany(x => x.Personality.Select(c => new {c.Name, c.Percentile, c.Details}))
                    .GroupBy(x => x.Name).Select(c => new
                    {
                        name = c.Key,
                        percentile = c.Average(p => p.Percentile),
                        details = c.SelectMany(d => d.Details)
                            .GroupBy(e => e.Name)
                            .Select(f => new {name = f.Key, percentile = f.Average(g => g.Percentile)})
                            .ToList()
                    });

                var values = personalities.SelectMany(x => x.Values.Select(c => new {c.Name, c.Percentile}))
                    .GroupBy(x => x.Name).Select(c => new {name = c.Key, percentile = c.Average(p => p.Percentile)});

                var result = new
                {
                    mentions,
                    sources,
                    needs,
                    personality,
                    values,
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public dynamic PostAnalyze([FromBody]AnalyseRequest entity)
        {
            if (entity.Brand == "" || entity.Briefing == "" || entity.Factor == "" || entity.Product == "")
                return BadRequest("Dados inválidos");


            var analisys = PersonalityService.GetPersonalityResult(entity.Briefing);
            var briefing = new Briefing
            {
                Brand = entity.Brand,
                Description = entity.Briefing,
                Factor = entity.Factor,
                Product = entity.Product,
                Analysis = JsonConvert.SerializeObject(analisys)
            };
            
            _dbContext.Briefings.Add(briefing);

            _dbContext.SaveChanges();

            return Ok(new
            {
                briefing.Id,
                Briefing = briefing.Description,
                briefing.Product,
                briefing.Brand,
                briefing.Factor,
                Personality = analisys.Personality,
                Values = analisys.Values,
                Needs = analisys.Needs
            });
            
        }
    }
}