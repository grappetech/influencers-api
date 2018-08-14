using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.Models.Core;
using Action.Models.Scrap;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Action.Controllers
{
	[Route("api/instagram")]

	[EnableCors("Default")]
	[AllowAnonymous]
	public class InstagramController : BaseController
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public InstagramController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
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

		private InstagramResultViewModel Mock(int id)
		{
			var social = _dbContext.SocialData
				.OrderByDescending(x=>x.Date)
				.FirstOrDefault(x => x.EntityId == id && x.Network == ESocialNetwork.Instagram);
			return new InstagramResultViewModel
			{
				Engagement = Convert.ToDouble(social?.Engagement ?? 0),
				Likes = social?.Interactions ?? 0,
				Followers = social?.Followers ?? 0
			};
		}
	}
}