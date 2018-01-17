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
			return new[]
			{
				new StateSocialResultViewModel
				{
					Name = "Acre",
					Score = 0.6952,
				},
				new StateSocialResultViewModel
				{
					Name = "Alagoas",
					Score = 0.5698
				},
				new StateSocialResultViewModel
				{
					Name = "Amapá",
					Score = 0.4695
				},
				new StateSocialResultViewModel
				{
					Name = "Amazonas",
					Score = 0.5829
				},
				new StateSocialResultViewModel
				{
					Name = "Bahia",
					Score = 0.4569
				},
				new StateSocialResultViewModel
				{
					Name = "Ceará",
					Score = 0.5425
				},
				new StateSocialResultViewModel
				{
					Name = "Distrito Federal",
					Score = 0.5201
				},
				new StateSocialResultViewModel
				{
					Name = "Espírito Santo",
					Score = 0.4692
				},
				new StateSocialResultViewModel
				{
					Name = "Goiás",
					Score = 0.4859
				},
				new StateSocialResultViewModel
				{
					Name = "Maranhão",
					Score = 0.4955
				},
				new StateSocialResultViewModel
				{
					Name = "Mato Grosso",
					Score = 0.5102
				},
				new StateSocialResultViewModel
				{
					Name = "Mato Grosso do Sul",
					Score = 0.5220
				},
				new StateSocialResultViewModel
				{
					Name = "Minas Gerais",
					Score = 0.4459
				},
				new StateSocialResultViewModel
				{
					Name = "Pará",
					Score = 0.4752
				},
				new StateSocialResultViewModel
				{
					Name = "Paraíba",
					Score = 0.4658
				},
				new StateSocialResultViewModel
				{
					Name = "Paraná",
					Score = 0.4985
				},
				new StateSocialResultViewModel
				{
					Name = "Pernambuco",
					Score = 0.5210
				},
				new StateSocialResultViewModel
				{
					Name = "Piauí",
					Score = 0.4852
				},
				new StateSocialResultViewModel
				{
					Name = "Rio de Janeiro",
					Score = 0.5236
				},
				new StateSocialResultViewModel
				{
					Name = "Rio Grande do Norte",
					Score = 0.4985
				},
				new StateSocialResultViewModel
				{
					Name = "Rio Grande do Sul",
					Score = 0.4458
				},
				new StateSocialResultViewModel
				{
					Name = "Rondônia",
					Score = 0.5236
				},
				new StateSocialResultViewModel
				{
					Name = "Roraima",
					Score = 0.5102
				},
				new StateSocialResultViewModel
				{
					Name = "Santa Catarina",
					Score = 0.4985
				},
				new StateSocialResultViewModel
				{
					Name = "São Paulo",
					Score = 0.5125
				},
				new StateSocialResultViewModel
				{
					Name = "Sergipe",
					Score = 0.4859
				},
				new StateSocialResultViewModel
				{
					Name = "Tocantins",
					Score = 0.4952
				}
			}.ToList();
		}
	}
}