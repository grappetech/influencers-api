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
			return ValidateUser(() => Ok(Mock()));
		}

		private InstagramResultViewModel Mock()
		{
			return new InstagramResultViewModel
			{
				Followers = 223652,
				Engagement = 0.542,
				Likes = 3321252,
				AgeRanges = new AgeRangesViewModel
				{
					Age18_24 = 0.5412,
					Age25_31 = 0.4932,
					Age32_38 = 0.4562,
					Age39_45 = 0.4262,
					Age46_52 = 0.4125,
					Age53_59 = 0.3012,
					Age60 = 0.2592
				},
				Stats = new[]
				{
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-5).ToString("MM-yyyy"),
						Followers = 100,
						Retweets = 106,
						Engagement = 0.434,
						Likes = 122
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-4).ToString("MM-yyyy"),
						Followers = 104,
						Retweets = 108,
						Engagement = 0.442,
						Likes = 128
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-3).ToString("MM-yyyy"),
						Followers = 112,
						Retweets = 110,
						Engagement = 0.482,
						Likes = 137
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-2).ToString("MM-yyyy"),
						Followers = 115,
						Retweets = 112,
						Engagement = 0.503,
						Likes = 147
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.AddMonths(-1).ToString("MM-yyyy"),
						Followers = 117,
						Retweets = 118,
						Engagement = 0.523,
						Likes = 126
					},
					new SocialStatViewModel
					{
						Month = DateTime.Today.ToString("MM-yyyy"),
						Followers = 125,
						Retweets = 130,
						Engagement = 0.542,
						Likes = 127
					}
				}.ToList()
			};
		}
	}
}