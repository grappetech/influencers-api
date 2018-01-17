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
	[Route("api/facebook")]

	[EnableCors("Default")]
	[AllowAnonymous]
	public class FacebookController : BaseController
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public FacebookController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		[HttpGet("entities/{id}")]
		public IActionResult Get([FromRoute] int id)
		{
			return ValidateUser(() => Ok(Mock()));
		}

		private FacebookResultViewModel Mock()
		{
			return new FacebookResultViewModel
			{
				Followers = 152152,
				Engagement = 0.452,
				Likes = 1252512,
				AgeRanges = new AgeRangesViewModel
				{
					Age18_24 = 0.4985,
					Age25_31 = 0.4825,
					Age32_38 = 0.4462,
					Age39_45 = 0.4160,
					Age46_52 = 0.3921,
					Age53_59 = 0.3595,
					Age60 = 0.3021
				},
				Stats = new[]
				{
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-5).ToString("MM-yyyy"),
						Followers = 105,
						Retweets = 111,
						Engagement = 0.439,
						Likes = 127
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-4).ToString("MM-yyyy"),
						Followers = 109,
						Retweets = 113,
						Engagement = 0.447,
						Likes = 134
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-3).ToString("MM-yyyy"),
						Followers = 117,
						Retweets = 115,
						Engagement = 0.490,
						Likes = 141
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-2).ToString("MM-yyyy"),
						Followers = 120,
						Retweets = 117,
						Engagement = 0.553,
						Likes = 151
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-1).ToString("MM-yyyy"),
						Followers = 117,
						Retweets = 118,
						Engagement = 0.523,
						Likes = 135
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.ToString("MM-yyyy"),
						Followers = 119,
						Retweets = 135,
						Engagement = 0.512,
						Likes = 140
					}
				}.ToList()
			};
		}
	}
}