using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.Models.Scrap;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
	//[Authorize]

	[EnableCors("Default")]
	[Route("api/[controller]")]
	public class ScrapSourcesController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public ScrapSourcesController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		// GET: api/values
		[HttpGet("alias/{source}")]
		public IEnumerable<ScrapSource> Get([FromRoute] string source)
		{
			if (_dbContext == null)
				return null;
			return _dbContext.ScrapSources.Where(x => x.Alias.ToLower().Contains(source.ToLower()) && x.PageStatus != EPageStatus.Error).ToList();
		}

		// GET: api/values
		[HttpGet("{id}")]
		public ScrapSource GetById([FromRoute] int id)
		{
			if (_dbContext == null)
				return null;
			return _dbContext.ScrapSources.Find(id);
		}

		// GET: api/values
		[HttpGet("")]
		public IEnumerable<ScrapSource> Get()
		{
			if (_dbContext == null)
				return null;
			return _dbContext.ScrapSources.Where(x => x.PageStatus != EPageStatus.Error).ToList();
		}

		// POST api/values
		[HttpPost]
		public ScrapSource Post([FromBody] ScrapSource model)
		{
			if (_dbContext == null)
				return null;
			_dbContext.ScrapSources.Add(model);
			_dbContext.SaveChanges();
			return model;
		}

		// PUT api/values
		[HttpPut("")]
		public ScrapSource Put([FromBody] ScrapSource model)
		{
			if (_dbContext == null)
				return null;
			_dbContext.Entry(model).State = EntityState.Modified;
			_dbContext.SaveChanges();
			return model;
		}

		// DELETE api/values
		[HttpDelete]
		public ScrapSource Delete([FromBody] ScrapSource model)
		{
			if (_dbContext == null)
				return null;
			var item = _dbContext.ScrapSources.Find(model.Id);
			_dbContext.ScrapSources.Remove(item);
			_dbContext.SaveChanges();
			return model;
		}

		[HttpGet("pages/{id}")]
		public IEnumerable<ScrapedPage> GetPages(int id)
		{
			if (_dbContext == null)
			{
				return null;
			}
			var item = _dbContext.ScrapedPages.Where(x => x.ScrapSourceId == id);
			return item.ToList();
		}
	}
}