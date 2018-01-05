using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Action.Models;
using System.Text.Encodings.Web;

namespace Action.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("Default")]
	public class IndustriesController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public IndustriesController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		[HttpGet]
		public dynamic Get()
		{
			try
			{
				if (_dbContext == null)
					return NotFound("No database connection");

				var data = _dbContext.Industries.ToList();

				return data.ToList();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}