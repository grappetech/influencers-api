using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Action.Extensions;
using Action.Models;
using Action.Models.Core;
using Action.Models.Watson.NLU;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Entity = Action.Models.Watson.Entity;

namespace Action.Controllers
{
    [Route("api/entities")]
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
        [HttpGet("")]
        public dynamic Get([FromQuery] string name = "")
        {
            try
            {
                if (_dbContext == null)
                    return NotFound("No database connection");
                var data = _dbContext.Entities.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

                List<EntityViewModel> lEntityViewModel = new List<EntityViewModel>();
                if (data != null)
                    foreach (var d in data)
                        lEntityViewModel.Add(new EntityViewModel
                        {
                            Id = d.Id,
                            Entity = d.Name,
                            Type = d.Category,
                            ImageUrl = d.PictureUrl,
                        });
                return lEntityViewModel;
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

                if (data == null)
                    return StatusCode((int) EServerError.BusinessError,
                        new List<string> {"Object not found with ID " + id.ToString() + "."});

                return new EntityViewModel
                {
                    Id = data.Id,
                    Entity = data.Name,
                    Type = data.Category,
                    ImageUrl = data.PictureUrl
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{id}/image")]
        public async Task<IActionResult> PostFile([FromRoute] int id, [FromForm] IFormFile file)
        {
            try
            {
                var fileId = Guid.NewGuid().ToString();

                if (file == null || file.Length == 0)
                    return Content("file not selected");

                if (file == null || file.FileName.Equals(""))
                    return Content("file not selected");

                if (_dbContext == null)
                    return NotFound("No database connection");

                var data = _dbContext.Entities.FirstOrDefault(x => x.Id == id);

                if (data == null)
                    return StatusCode((int) EServerError.BusinessError,
                        new List<string> {$"Entity not found with ID {id}."});

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    data.PictureUrl = ImageUpload.GenerateFileRoute(fileId + file.FileName.GetFileExtension(), stream,
                        Request, _dbContext);
                }

                _dbContext.Entry(data).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return Ok(new {imageUrl = data.PictureUrl});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("image/{filename}")]
        public async Task<IActionResult> GetImage([FromRoute] string filename)
        {
            var img = await _dbContext.Images.FirstOrDefaultAsync(x => x.ImageName.Equals(filename));
            var memory = new MemoryStream(Convert.FromBase64String(img.Base64Image)) {Position = 0};
            return File(memory, filename.GetMimeType(), filename);
        }


        // POST api/values
        [HttpPost]
        public dynamic Post([FromBody] Entity model)
        {
            try
            {
                if (_dbContext == null)
                {
                    return NotFound("No database connection");
                }

                var dt = _dbContext.Entities.FirstOrDefault(x => x.Name.Equals(model.Name));

                if (dt != null)
                    return new EntityViewModel
                    {
                        Id = dt.Id,
                        Entity = dt.Name,
                        Type = dt.Category,
                        ImageUrl = dt.PictureUrl
                    };

                var entity = new Entity
                {
                    Alias = model.Alias,
                    CategoryId = ECategory.Brand,
                    Date = DateTime.Today,
                    Name = model.Name,
                    FacebookUser = model.FacebookUser,
                    InstagranUser = model.InstagranUser,
                    PictureUrl = model.PictureUrl,
                    SiteUrl = model.SiteUrl,
                    TweeterUser = model.TweeterUser,
                    YoutubeUser = model.YoutubeUser
                };

                var data = _dbContext.Entities.Add(entity);
                _dbContext.SaveChanges();

                return new EntityViewModel
                {
                    Id = data.Entity.Id,
                    Entity = data.Entity.Name,
                    Type = data.Entity.Category,
                    ImageUrl = data.Entity.PictureUrl
                };
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

        [HttpGet("{id}/personality")]
        public dynamic GetPersonality([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            long lid = id;
            
            try
            {
                var scrapdPages = _dbContext.ScrapedPages.Where(x =>
                    x.Status == EDataExtractionStatus.Finalized);// && x.Date >= from && x.Date <= to);

                var pagesId = scrapdPages.Select(x => x.Id).ToList();

                var Personalities = _dbContext.Personalities
                    .Include(x => x.Personality)
                    .ThenInclude(x => x.Details)
                    .Where(x => x.EntityId == lid && pagesId.Contains(x.ScrapedPageId))
                    .ToList();

                var Personality = Personalities
                    .SelectMany(x => x.Personality.Select(c => new {c.Name, c.Percentile, c.Details}))
                    .GroupBy(x => x.Name).Select(c => new
                    {
                        name = c.Key,
                        percentile = c.Average(p => p.Percentile),
                        details = c.SelectMany(d => d.Details)
                            .GroupBy(e => e.Name)
                            .Select(f => new {name = f.Key, percentile = f.Average(g => g.Percentile)})
                            .ToList()
                    }).ToList();

                var result = Personality.Select(x => new PersonalityDescriptionViewModel
                {
                    Name =x.name, 
                    Percentile = x.percentile, 
                    Details = x.details.Select(d=>new PersonalityDetailDescriptionViewModel
                    {
                        Name = d.name,
                        Percentile = d.percentile
                    }).ToList()
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/values")]
        public dynamic GetValues([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            
            long lid = id;
            
            try
            {
                var scrapdPages = _dbContext.ScrapedPages.Where(x =>
                    x.Status == EDataExtractionStatus.Finalized);// && x.Date >= from && x.Date <= to);

                var pagesId = scrapdPages.Select(x => x.Id).ToList();

                var Personalities = _dbContext.Personalities
                    .Include(x => x.Values)
                    .Where(x => x.EntityId == lid && pagesId.Contains(x.ScrapedPageId))
                    .ToList();

                var Personality = Personalities
                    .SelectMany(x => x.Values.Select(c => new {c.Name, c.Percentile}))
                    .GroupBy(x => x.Name).Select(c => new
                    {
                        name = c.Key,
                        percentile = c.Average(p => p.Percentile),
                        description = ""
                    });

                var result = Personality.Select(x => new {x.name, x.percentile, x.description});

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/tones")]
        public dynamic GetTones([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            long lid = id;
            try
            {
                var scrapdPages = _dbContext.ScrapedPages.Where(x =>
                    x.Status == EDataExtractionStatus.Finalized);// && x.Date >= from && x.Date <= to);

                var pagesId = scrapdPages.Select(x => x.Id).ToList();

                var tones = _dbContext.Tones.Where(x => pagesId.Contains(x.ScrapedPageId) && x.EntityId.HasValue && x.EntityId.Value == lid).Select(
                    x => new
                    {
                        scrapdPages.FirstOrDefault(y => y.Id == x.ScrapedPageId).Url,
                        x.SetenceTones,
                        scrapdPages.FirstOrDefault(y => y.Id == x.ScrapedPageId).Date,
                        Mentions = x.SetenceTones.Select(z => new
                        {
                            z.Id,
                            z.Text,
                            tone = GetMaxTone(z.ToneCategories.SelectMany(y => y.Tones).Select(t => new
                            {
                                t.Score,
                                id = t.ToneId,
                                name = t.ToneName
                            }))
                        })
                    });

                var stones = tones.SelectMany(x => x.SetenceTones).Select(x => new
                {
                    x.Text,
                    tones = x.ToneCategories.SelectMany(y => y.Tones).Select(z => new
                    {
                        z.Score,
                        Id = z.ToneId,
                        Name = z.ToneName
                    })
                });

                var resultTones = new List<ToneItem>();

                foreach (var item in stones)
                {
                    ToneItem tom = new ToneItem();

                    foreach (var item2 in item.tones)
                    {
                        if (item2.Score > tom.Score)
                        {
                            tom.Score = item2.Score.Value;
                            tom.Id = item2.Id;
                            tom.Name = item2.Name;
                        }
                    }

                    if (tom.Score > 0)
                        resultTones.Add(tom);
                }

                var result = resultTones.GroupBy(x => x.Id).Select(x => new
                {
                    x.Key,
                    avg = x.Average(y => y.Score)
                }).Select(x => new
                {
                    score = x.avg,
                    id = x.Key,
                    name = resultTones.FirstOrDefault(y => y.Id.Equals(x.Key)).Name
                });


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
        [HttpGet("{id}/relations")]
        public IActionResult GetRelations([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to,
            [FromQuery] string word, [FromQuery] string relationshipFactor, [FromQuery] string type)
        {
            try
            {

                var pageIds = _dbContext.ScrapedPages
                    .Select(x => x.Id);
                    //.Where(x => x.Date >= from && x.Date <= to)

                

                    var query = _dbContext.NluResults
                        .Include(x => x.Entity)
                        .Include(x => x.Relations)
                        .ThenInclude(x => x.Arguments)
                        .Where(x => x.Entity.Any(z => z.EntityId == id) &&
                                    x.ScrapedPageId != null &&
                                    x.Relations.Any(z => z.type.ToLower().Equals(relationshipFactor.ToLower())) &&
                                    pageIds.Contains(x.ScrapedPageId));

                    var list = query.ToList()
                        .SelectMany(x => x.Relations)
                        .GroupBy(x => x.sentence)
                        .Select(x => new WordViewModel
                        {
                            Id = x.Select(c => c.Id.ToString()).Min(),
                            Text = x.Key,
                            Weight = Convert.ToInt32(x.Select(c => c.score ?? 0.1F).Sum() * 1000),
                            Type = GetEmotion(new
                            {
                                anger = 0,
                                disgust = 0,
                                joy = 0,
                                sadness = 0,
                                fear = 0
                            })
                        })
                        .ToList();


                    return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode((int) EServerError.BusinessError, new List<string> {ex.Message});
            }
        }
        
        
        [HttpGet("{id}/mentions")]
        public IActionResult GetMentions([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to,
            [FromQuery] string word, [FromQuery] int relationshipFactor, [FromQuery] string type)
        {
            try
            {
               var scrapdPages = _dbContext.ScrapedPages;//.Where(x =>  x.Date >= from && x.Date <= to);

                var pagesId = scrapdPages.Select(x => x.Id).ToList();

                var tones = _dbContext.Tones.Where(x => pagesId.Contains(x.ScrapedPageId) && x.EntityId == id)
                	.Select(x => new
                	{
                		scrapdPages.FirstOrDefault(y => y.Id == x.ScrapedPageId).Url,
                		scrapdPages.FirstOrDefault(y => y.Id == x.ScrapedPageId).Date,
                		Mentions = x.SetenceTones.Select(z => new
                		{
                			z.Id,
                			z.Text,
                			tone = GetMaxTone(z.ToneCategories.SelectMany(y => y.Tones).Select(t => new
                			{
                				t.Score,
                				id = t.ToneId,
                				name = t.ToneName
                			}))
                		})
                	});

                var result = tones.SelectMany(x => x.Mentions.Select(y => new
                {
                	id = y.Id,
                	text = y.Text,
                	toneId = y.tone.Id,
                	url = x.Url,
                	type = y.tone.ToneType.ToString(),
                	date = x.Date
                }));

                if (word != null && !word.Equals(""))
                	result = result.Where(x => x.text.Contains(word));
                if (type != null && !type.Equals(""))
                	result = result.Where(x => x.type.Equals(type));
                if (relationshipFactor > 0)
                { }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int) EServerError.BusinessError, new List<string> {ex.Message});
            }
        }

        [HttpGet("{id}/feeling")]
        public IActionResult GetFeeling([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                return Ok(this.MockFeeling());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/words")]
        public IActionResult GetWords([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] string relationshipFactor = "")
        {
            try
            {

                var pageIds = _dbContext.ScrapedPages
                    .Select(x => x.Id);
                    //.Where(x => x.Date >= from && x.Date <= to)

                if (string.IsNullOrWhiteSpace(relationshipFactor) || relationshipFactor.ToLower().Equals("undefined"))
                {

                    var list = _dbContext.NluResults
                        .Include(x => x.Entity)
                        .Include(x => x.Keywords)
                        .ThenInclude(x => x.emotions)
                        .Where(x => x.Entity.Any(z => z.EntityId == id) &&
                                    x.ScrapedPageId != null &&
                                    pageIds.Contains(x.ScrapedPageId))
                        .ToList()
                        .SelectMany(x => x.Keywords)
                        .GroupBy(x => x.text)
                        .Select(x => new WordViewModel
                        {
                            Id = x.Select(c => c.Id.ToString()).Min(),
                            Text = x.Key,
                            Weight = Convert.ToInt32(x.Select(c => c.relevance ?? 0.1F).Sum() * 1000),
                            Type = GetEmotion(new
                            {
                                anger = x.Select(c => c.emotions.anger ?? 0.1F).Average(),
                                disgust = x.Select(c => c.emotions.disgust ?? 0.1F).Average(),
                                joy = x.Select(c => c.emotions.joy ?? 0.1F).Average(),
                                sadness = x.Select(c => c.emotions.sadness ?? 0.1F).Average(),
                                fear = x.Select(c => c.emotions.fear ?? 0.1F).Average()
                            })
                        })
                        .ToList();


                    return Ok(list);
                }
                else
                {

                    var list = _dbContext.NluResults
                        .Include(x => x.Entity)
                        .Include(x => x.Relations)
                        .ThenInclude(x => x.Arguments)
                        .Where(x => x.Entity.Any(z => z.EntityId == id) &&
                                    x.ScrapedPageId != null &&
                                    x.Relations.Any(z=>z.type.ToLower().Equals(relationshipFactor.ToLower())) &&
                                    pageIds.Contains(x.ScrapedPageId))
                        .ToList()
                        .SelectMany(x => x.Relations)
                        .GroupBy(x => x.sentence)
                        .Select(x => new WordViewModel
                        {
                            Id = x.Select(c => c.Id.ToString()).Min(),
                            Text = x.Key,
                            Weight = Convert.ToInt32(x.Select(c => c.score ?? 0.1F).Sum() * 1000),
                            Type = GetEmotion(new
                            {
                                anger = 0,
                                disgust = 0,
                                joy = 0,
                                sadness = 0,
                                fear = 0
                            })
                        })
                        .ToList();


                    return Ok(list);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int) EServerError.BusinessError, new List<string> {ex.Message});
            }
        }

        private ToneItem GetMaxTone(IEnumerable<dynamic> enumerable)
        {
            ToneItem tom = new ToneItem();
            foreach (var item2 in enumerable)
            {
                if ((double) item2.Score > tom.Score)
                {
                    tom.Score = (double) item2.Score;
                    tom.Id = Convert.ToString(item2.id);
                    tom.Name = Convert.ToString(item2.name);
                }
            }

            if (tom.Score > 0)
                return tom;
            return null;
        }
        private string GetEmotion(dynamic xEmotions)
        {
            var positive = (xEmotions.sadness ?? 0.0 + xEmotions.joy ?? 0.0)/2.0;
            var negative = (xEmotions.anger ?? 0.0 + xEmotions.disgust ?? 0.0 + xEmotions.fear ?? 0.0) / 3.0;

            return positive < negative && (positive / negative) > 0.20 ?
                "negative" : positive > negative && (negative / positive) > 0.20 ?  "positive" : "negative";

        }

        private List<MentionMock> MockMentions(long id)
        {
            try
            {
                var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data",
                    "mock_mentions_result_" + id.ToString() + ".json"));
                return JsonConvert.DeserializeObject<List<MentionMock>>(json);
            }
            catch
            {
                var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data",
                    "mock_mentions_result.json"));
                return JsonConvert.DeserializeObject<List<MentionMock>>(json);
            }
        }

        private Object MockFeeling()
        {
            Random random = new Random(Randomize.Next());

            return new
            {
                positive = Math.Round(random.NextDouble(), 4),
                negative = Math.Round(random.NextDouble(), 4),
                neutro = Math.Round(random.NextDouble(), 4),
                mentions = random.Next(20, 150),
                sources = random.Next(100, 300)
            };
        }


        

        private List<WordMock> MockWords(long id)
        {
            return new List<WordMock>();
        }
    }

    internal class WordMock
    {
        public string id { get; set; }
        public string text { get; set; }
        public int weight { get; set; }
        public string type { get; set; }
    }


    internal class RelationsEntities
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal score { get; set; }
        public List<MentionMock> mentions { get; set; } 
    }

    internal class MentionMock
    {
        public string id { get; set; }
        public string url { get; set; }
        public string text { get; set; }
        public string toneId { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
    }
}