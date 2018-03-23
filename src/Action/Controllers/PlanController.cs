using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Action.Controllers
{
	[Route("api/[controller]")]

	[EnableCors("Default")]
	[AllowAnonymous]
	public class PlansController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public PlansController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		// GET: api/values
		[HttpGet]
		public dynamic Get()
		{
			try
			{
				if (_dbContext == null)
					return NotFound("No database connection");
				var data = _dbContext.Plans.Include(x => x.Features).ToList();

				List<PlanViewModel> lPlans = new List<PlanViewModel>();
				foreach (var d in data)
				{
					PlanViewModel lPlan = new PlanViewModel
					{
						Id = d.Id,
						Name = d.Name,
						Price = d.Price,
						Slug = d.Slug,
					};
					d.Features.ForEach(x => lPlan.Features.Add(new FeatureViewModel { Description = x.Description }));
					lPlans.Add(lPlan);
				}
				return lPlans;
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}