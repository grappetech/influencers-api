using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Action.Controllers
{
	[Route("api/social-states")]

	[EnableCors("Default")]
	[AllowAnonymous]
	public class SocialStateController : BaseController
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public SocialStateController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		[HttpGet("entities/{id}")]
		public IActionResult Get([FromRoute] int id)
		{
			return ValidateUser(() => Ok(Mock()));
		}

		private List<StateSocialResultViewModel> Mock()
		{
			Random random = new Random(Randomize.Next());
			return new[]
			{
				new StateSocialResultViewModel
				{
					Name = "Acre",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Alagoas",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Amapá",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Amazonas",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Bahia",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Ceará",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Distrito Federal",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Espírito Santo",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Goiás",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Maranhão",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Mato Grosso",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Mato Grosso do Sul",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Minas Gerais",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Pará",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Paraíba",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Paraná",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Pernambuco",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Piauí",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Rio de Janeiro",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Rio Grande do Norte",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Rio Grande do Sul",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Rondônia",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Roraima",
					Score = 0.5102
				},
				new StateSocialResultViewModel
				{
					Name = "Santa Catarina",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "São Paulo",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Sergipe",
					Score = Math.Round(random.NextDouble(), 4),
				},
				new StateSocialResultViewModel
				{
					Name = "Tocantins",
					Score = Math.Round(random.NextDouble(), 4),
				}
			}.ToList();
		}
	}
}