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
using Newtonsoft.Json;
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
			try
			{
				return ValidateUser(() => Ok(Mock(id)));
			}
			catch (Exception ex)
			{
				return StatusCode((int)EServerError.BusinessError, new List<string> { ex.Message });
			}
		}

		private List<StateSocialResultViewModel> Mock(int id)
		{
			try
			{
				var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data", "mock_social_state_result_" + id.ToString() + ".json"));
				return JsonConvert.DeserializeObject<List<StateSocialResultViewModel>>(json);
			}
			catch
			{
				var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data", "mock_social_state_result.json"));
				return JsonConvert.DeserializeObject<List<StateSocialResultViewModel>>(json);
			}
		}
	}
}