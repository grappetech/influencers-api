using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Action.Extensions;
using Action.Models;
using Action.Models.Watson;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

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

                return Ok(new {ImageURL = data.PictureUrl});
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
            switch (id)
            {
                case 1:
                    lid = 1192;
                    break;
                case 6609:
                    lid = 1192;
                    break;
                case 3768:
                    lid = 1178;
                    break;
                case 6602:
                    lid = 247;
                    break;
                default:
                    lid = id;
                    break;
            }

            try
            {
                var scrapdPages = _dbContext.ScrapedPages.Where(x =>
                    x.Status == EDataExtractionStatus.Finalized && x.Date >= from && x.Date <= to);

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
                            .Select(f => new {Name = f.Key, Percentile = f.Average(g => g.Percentile)})
                            .ToList()
                    });

                var result = Personality.Select(x => new {x.name, x.percentile, x.details});

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
            switch (id)
            {
                case 1:
                    lid = 1192;
                    break;
                case 6609:
                    lid = 1192;
                    break;
                case 3768:
                    lid = 1178;
                    break;
                case 6602:
                    lid = 247;
                    break;
                default:
                    lid = id;
                    break;
            }
            
            try
            {
                var scrapdPages = _dbContext.ScrapedPages.Where(x =>
                    x.Status == EDataExtractionStatus.Finalized && x.Date >= from && x.Date <= to);

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
            switch (id)
            {
                case 1:
                    lid = 1206;
                    break;
                case 6609:
                    lid = 1206;
                    break;
                case 3768:
                    lid = 212;
                    break;
                case 6602:
                    lid = 226;
                    break;
                default:
                    lid = id;
                    break;
            }
            
            try
            {
                var scrapdPages = _dbContext.ScrapedPages.Where(x =>
                    x.Status == EDataExtractionStatus.Finalized && x.Date >= from && x.Date <= to);

                var pagesId = scrapdPages.Select(x => x.Id).ToList();

                var tones = _dbContext.Tones.Where(x => pagesId.Contains(x.ScrapedPageId) && x.EntityId == lid).Select(
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

        [HttpGet("{id}/mentions")]
        public IActionResult GetMentions([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to,
            [FromQuery] string word, [FromQuery] int relationshipFactor, [FromQuery] string type)
        {
            try
            {
                return Ok(this.MockMentions(id));

                //var scrapdPages = _dbContext.ScrapedPages.Where(x => x.Status == EDataExtractionStatus.Finalized && x.Date >= from && x.Date <= to);

                //var pagesId = scrapdPages.Select(x => x.Id).ToList();

                //var tones = _dbContext.Tones.Where(x => pagesId.Contains(x.ScrapedPageId) && x.EntityId == id)
                //	.Select(x => new
                //	{
                //		scrapdPages.FirstOrDefault(y => y.Id == x.ScrapedPageId).Url,
                //		scrapdPages.FirstOrDefault(y => y.Id == x.ScrapedPageId).Date,
                //		Mentions = x.SetenceTones.Select(z => new
                //		{
                //			z.Id,
                //			z.Text,
                //			tone = GetMaxTone(z.ToneCategories.SelectMany(y => y.Tones).Select(t => new
                //			{
                //				t.Score,
                //				id = t.ToneId,
                //				name = t.ToneName
                //			}))
                //		})
                //	});

                //var result = tones.SelectMany(x => x.Mentions.Select(y => new
                //{
                //	id = y.Id,
                //	text = y.Text,
                //	toneId = y.tone.Id,
                //	url = x.Url,
                //	type = y.tone.ToneType.ToString(),
                //	date = x.Date
                //}));

                //if (word != null && !word.Equals(""))
                //	result = result.Where(x => x.text.Contains(word));
                //if (type != null && !type.Equals(""))
                //	result = result.Where(x => x.type.Equals(type));
                //if (relationshipFactor > 0)
                //{ }

                //return Ok(result);
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
        public IActionResult GetWords([FromRoute] long id, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                return Ok(this.MockWords(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int) EServerError.BusinessError, new List<string> {ex.Message});
            }
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
            try
            {
                var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data",
                    "mock_words_result_" + id.ToString() + ".json"));
                return JsonConvert.DeserializeObject<List<WordMock>>(json);
            }
            catch
            {
                var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data",
                    "mock_words_result.json"));
                return JsonConvert.DeserializeObject<List<WordMock>>(json);
            }
        }

        private dynamic MockPersonality(int id)
        {
            try
            {
                var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data",
                    "mock_words_result_" + id.ToString() + ".json"));
                return JsonConvert.DeserializeObject<List<WordMock>>(json);
            }
            catch
            {
                var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data",
                    "mock_words_result.json"));
                return JsonConvert.DeserializeObject<List<WordMock>>(json);
            }
        }
    }

    internal class WordMock
    {
        public string id { get; set; }
        public string text { get; set; }
        public int weight { get; set; }
        public string type { get; set; }
    }

    internal class MentionMock
    {
        public string id { get; set; }
        public string text { get; set; }
        public string toneId { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
    }
}