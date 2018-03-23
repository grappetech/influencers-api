using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Action.Models;
using Action.Models.Core;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Action.Controllers
{
	[Route("api/social-general-public")]

	[EnableCors("Default")]
	[AllowAnonymous]
	public class SocialPublicController : BaseController
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public SocialPublicController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
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

		private SocialPublicViewModel Mock(int id)
		{
			try
			{
				var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data", "mock_social_general_public_" + id.ToString() + ".json"));
				return JsonConvert.DeserializeObject<SocialPublicViewModel>(json);
			}
			catch
			{
				var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data", "mock_social_general_public.json"));
				return JsonConvert.DeserializeObject<SocialPublicViewModel>(json);
			}
		}
	}
}