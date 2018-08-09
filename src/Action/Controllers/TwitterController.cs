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
	[Route("api/twitter")]

	[EnableCors("Default")]
	[AllowAnonymous]
	public class TwitterController : BaseController
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public TwitterController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
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

		private TwitterResultViewModel Mock(int id)
		{
			var social = _dbContext.SocialData.OrderByDescending(x=>x.Date).FirstOrDefault(x => x.EntityId == id && x.Network == ESocialNetwork.Twitter);
			return new TwitterResultViewModel
			{
				Engagement = Convert.ToDouble(social?.Engagement ?? 0),
				Retweets = social?.Interactions ?? 0,
				Followers = social?.Followers ?? 0
			};
		}
	}
}