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
			return ValidateUser(() => Ok(Mock()));
		}

		private TwitterResultViewModel Mock()
		{
			return new TwitterResultViewModel
			{
				Followers = 12355,
				Retweets = 32215,
				Engagement = 0.423,
				Likes = 125652,
				AgeRanges = new AgeRangesViewModel
				{
					Age18_24 = 0.4706,
					Age25_31 = 0.4690,
					Age32_38 = 0.4562,
					Age39_45 = 0.4221,
					Age46_52 = 0.3921,
					Age53_59 = 0.3562,
					Age60 = 0.3103
				},
				Stats = new[]
				{
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-5).ToString("MM-yyyy"),
						Followers = 98,
						Retweets = 104,
						Engagement = 0.432,
						Likes = 120
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-4).ToString("MM-yyyy"),
						Followers = 102,
						Retweets = 106,
						Engagement = 0.440,
						Likes = 126
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-3).ToString("MM-yyyy"),
						Followers = 110,
						Retweets = 108,
						Engagement = 0.480,
						Likes = 135
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-2).ToString("MM-yyyy"),
						Followers = 113,
						Retweets = 110,
						Engagement = 0.501,
						Likes = 145
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-1).ToString("MM-yyyy"),
						Followers = 115,
						Retweets = 116,
						Engagement = 0.521,
						Likes = 124
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.ToString("MM-yyyy"),
						Followers = 123,
						Retweets = 128,
						Engagement = 0.540,
						Likes = 125
					}
				}.ToList()
			};
		}
	}
}