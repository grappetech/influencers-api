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
			Random random = new Random(Randomize.Next());
			return new FacebookResultViewModel
			{
				Followers = random.Next(123456, 300000),
				Engagement = Math.Round(random.NextDouble(), 4),
				Likes = random.Next(123456, 300000),
				AgeRanges = new AgeRangesViewModel
				{
					Age18_24 = Math.Round(random.NextDouble(), 4),
					Age25_31 = Math.Round(random.NextDouble(), 4),
					Age32_38 = Math.Round(random.NextDouble(), 4),
					Age39_45 = Math.Round(random.NextDouble(), 4),
					Age46_52 = Math.Round(random.NextDouble(), 4),
					Age53_59 = Math.Round(random.NextDouble(), 4),
					Age60 = Math.Round(random.NextDouble(), 4)
				},
				Stats = new[]
				{
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-5).ToString("MM-yyyy"),
						Followers = random.Next(100, 300),
						Retweets = random.Next(100, 300),
						Engagement = Math.Round(random.NextDouble(), 4),
						Likes = random.Next(100, 300)
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-4).ToString("MM-yyyy"),
						Followers = random.Next(100, 300),
						Retweets = random.Next(100, 300),
						Engagement = Math.Round(random.NextDouble(), 4),
						Likes = random.Next(100, 300)
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-3).ToString("MM-yyyy"),
						Followers = random.Next(100, 300),
						Retweets = random.Next(100, 300),
						Engagement = Math.Round(random.NextDouble(), 4),
						Likes = random.Next(100, 300)
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-2).ToString("MM-yyyy"),
						Followers = random.Next(100, 300),
						Retweets = random.Next(100, 300),
						Engagement = Math.Round(random.NextDouble(), 4),
						Likes = random.Next(100, 300)
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-1).ToString("MM-yyyy"),
						Followers = random.Next(100, 300),
						Retweets = random.Next(100, 300),
						Engagement = Math.Round(random.NextDouble(), 4),
						Likes = random.Next(100, 300)
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.ToString("MM-yyyy"),
						Followers = random.Next(100, 300),
						Retweets = random.Next(100, 300),
						Engagement = Math.Round(random.NextDouble(), 4),
						Likes = random.Next(100, 300)
					}
				}.ToList()
			};
		}
	}
}