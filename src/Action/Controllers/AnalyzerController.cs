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
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                var Mentions = _dbContext.EntityMentions.Count(x => x.EntityId == entity);
                var Sources = _dbContext.EntityMentions.Select(x => x.ScrapedPageId).Distinct().Count();
                var Personalities = _dbContext.Personalities
                    .Include(x => x.Personality)
                    .ThenInclude(x => x.Details)
                    .Include(x => x.Needs)
                    .Include(x => x.Values).Where(x => x.EntityId == entity).ToList();

                var Needs = Personalities.SelectMany(x => x.Needs.Select(c => new { c.Name, c.Percentile }))
                    .GroupBy(x => x.Name).Select(c => new { Name = c.Key, Percentile = c.Average(p => p.Percentile) });

                var Personality = Personalities
                    .SelectMany(x => x.Personality.Select(c => new { c.Name, c.Percentile, c.Details }))
                    .GroupBy(x => x.Name).Select(c => new
                    {
                        name = c.Key,
                        percentile = c.Average(p => p.Percentile),
                        details = c.SelectMany(d => d.Details)
                            .GroupBy(e => e.Name)
                            .Select(f => new { Name = f.Key, Percentile = f.Average(g => g.Percentile) })
                            .ToList()
                    });

                var Values = Personalities.SelectMany(x => x.Values.Select(c => new { c.Name, c.Percentile }))
                    .GroupBy(x => x.Name).Select(c => new { Name = c.Key, Percentile = c.Average(p => p.Percentile) });

                var result = new
                {
                    Mentions,
                    Sources,
                    Needs,
                    Personality,
                    Values,
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
        public dynamic GetTone([FromRoute]int entity, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var ltones = _dbContext.Tones.Where(x => x.EntityId == entity).ToList();
            for (var i = 0; i < ltones.Count; i++)
            {
                var lpage = _dbContext.ScrapedPages.Where(x => x.Status == EDataExtractionStatus.Finalized && x.Id == ltones[i].ScrapedPageId);
            }
            //return ltones; 
            return Ok(new
            {
                sources = 170, // Nova propriedade
                tone = new
                {
                    positive = 0.408,
                    negative = 0.59200000000000008,
                    neutro = 0.59200000000000008
                },
                tones = new[] {
                    new {
                        score= 0.570627,
                        id= "joy",
                        name= "Alegria"
                    },
                    new {
                        score= 0.570627,
                        id= "medo",
                        name= "Medo"
                    },
                    new {
                        score= 0.570627,
                        id= "raiva",
                        name= "raiva"
                    },
                    new {
                        score= 0.570627,
                        id= "nojo",
                        name= "nojo"
                    },
                    new {
                        score= 0.570627,
                        id= "tristeza",
                        name= "tristeza"
                    }
                },
                mentions = new[] {
                    new {
                        id= 0,
                        text= "And Munik Nunes went up to the altar, on Tuesday night (3), in the Pequeno Grande Church, in the central region of Fortaleza.",
                        toneId= "joy",
                        url= "https=//app.zeplin.io/project/59e65cce0aaf66ac77cd5bae/screen/59e902da6b03291016bd7756",
                        type= "positive",
                        date= "2017-10-28T00=00=00"
                    },
                    new {
                        id= 0,
                        text= "And Munik Nunes went up to the altar, on Tuesday night (3), in the Pequeno Grande Church, in the central region of Fortaleza.",
                        toneId= "joy",
                        url= "https=//app.zeplin.io/project/59e65cce0aaf66ac77cd5bae/screen/59e902da6b03291016bd7756",
                        type= "negative",
                        date= "2017-10-28T00=00=00"
                    },
                    new {
                        id= 0,
                        text= "And Munik Nunes went up to the altar, on Tuesday night (3), in the Pequeno Grande Church, in the central region of Fortaleza.",
                        toneId= "joy",
                        url= "https=//app.zeplin.io/project/59e65cce0aaf66ac77cd5bae/screen/59e902da6b03291016bd7756",
                        type= "neutro",
                        date= "2017-10-28T00=00=00"
                    }
                },
                words = new[] {
                    new {
                        id= "5243g67346h834j893",
                        text= "altar",
                        weight= 123,
                        type = "positive"
                    },
                    new {
                        id= "5243g67346h834j893",
                        text= "Tuesday",
                        weight= 2,
                        type = "neutral"
                    },
                    new {
                        id= "5243g67346h834j893",
                        text= "relationship",
                        weight= 33,
                        type = "negative"
                    }
                }
            });
        }


        //[Authorize]
        [HttpPost("analyze")]
        public dynamic PostAnalyze([FromBody]AnalyseRequest entity)
        {


            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_analyze_result.json"));
            var result = JsonConvert.DeserializeObject<dynamic>(json);
            result.Brand = entity.Brand;
            result.Product = entity.Product;
            return result;

            /*    
                if (entity.Brand == "" || entity.Briefing == "" || entity.Factor == "" || entity.Product == "")
                    return BadRequest("Dados inválidos");
    
    
                var analisys = PersonalityService.GetPersonalityResult(entity.Briefing);
                var briefing = new Briefing
                {
                    Id = 1,
                    Brand = entity.Brand,
                    Description = entity.Briefing,
                    Factor = entity.Factor,
                    Product = entity.Product,
                    Analysis = JsonConvert.SerializeObject(analisys)
                };
                
                //_dbContext.Briefings.Add(briefing);
    
                //_dbContext.SaveChanges();
    
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
                */
        }

        //[Authorize]
        [HttpGet("analyze/sentiment")]
        public dynamic GetSentiment([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_sentiment.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/sentimentdetail")]
        public dynamic GetSentimentDetail([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_sentimentdetail.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/personality")]
        public dynamic GetPersonality([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_personality.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/personalitydetail")]
        public dynamic GetPersonalityDetail([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_personalitydetail.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/relationdetail")]
        public dynamic GetRelationDetail([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_relationdetail.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/valuesdetail")]
        public dynamic GetValuesDetail([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_valuesdetail.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/sentencestonedetails")]
        public dynamic GetSentencesToneDetail([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_sentencestonedetail.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/cardrecomedation")]
        public dynamic GetCardRecomedation()
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_card_recomedation_artist.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/cardinformationrecomedation")]
        public dynamic GetCardInformationRecomedation()
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_cardinformation_recomendation_artist.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

        //[Authorize]
        [HttpGet("analyze/recomedation")]
        public dynamic GetRecomedation()
        {
            var json = System.IO.File.ReadAllText(
                Path.Combine(Startup.RootPath, "App_Data", "mock_recomendation.json"));
            return JsonConvert.DeserializeObject<dynamic>(json);
        }
    }
}