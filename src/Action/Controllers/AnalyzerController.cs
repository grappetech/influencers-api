using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.VewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Action.Models.Core;

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
					.GroupBy(x => x.Name).Select(c => new { Name = c.Key, Percentile = c.Average(p => p.Percentile), Description = "" });

				//var result = new
				//{
				//	Mentions,
				//	Sources,
				//	Needs,
				//	Personality,
				//	Values,
				//};

				var result = new
				{
					Personality,
					Values
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
			return Ok();/*
			try
			{
				var scrapdPages = _dbContext.ScrapedPages.Where(x => x.Status == EDataExtractionStatus.Finalized && x.Date >= from && x.Date <= to);

				var pagesId = scrapdPages.Select(x => x.Id).ToList();

				var tones = _dbContext.Tones.Where(x => pagesId.Contains(x.ScrapedPageId) && x.EntityId == entity).Select(x => new
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

				#region resultTones
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
				#endregion

				return Ok(new
				{
					sources = scrapdPages.Count(),

					tone = new
					{
						positive = GetProporcao(resultTones, EToneType.Positivo),
						negative = GetProporcao(resultTones, EToneType.Negativo),
						neutro = GetProporcao(resultTones, EToneType.Neutro)
					},

					tones = resultTones.GroupBy(x => x.Id).Select(x => new
					{
						x.Key,
						avg = x.Average(y => y.Score)
					}).Select(x => new
					{
						score = x.avg,
						id = x.Key,
						name = resultTones.FirstOrDefault(y => y.Id.Equals(x.Key)).Name
					}),

					mentions = tones.SelectMany(x => x.Mentions.Select(y => new
					{
						id = y.Id,
						text = y.Text,
						toneId = y.tone.Id,
						url = x.Url,
						type = y.tone.ToneType.ToString(),
						date = x.Date
					})),
				});
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}*/
		}

		private ToneItem GetMaxTone(IEnumerable<dynamic> enumerable)
		{
			ToneItem tom = new ToneItem();
			foreach (var item2 in enumerable)
			{
				if ((double)item2.Score > tom.Score)
				{
					tom.Score = (double)item2.Score.Value;
					tom.Id = Convert.ToString(item2.id);
					tom.Name = Convert.ToString(item2.name);
				}
			}
			if (tom.Score > 0)
				return tom;
			return null;
		}

		private decimal GetProporcao(List<ToneItem> pToneItens, EToneType pToneType)
		{
			return pToneItens.Count == 0 ? 0 : pToneItens.Count(x => x.ToneType == pToneType) / pToneItens.Count;
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