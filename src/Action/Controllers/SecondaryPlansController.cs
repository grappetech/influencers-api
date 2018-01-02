using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Action.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("Default")]
	public class SecondaryPlansController : Controller
	{
		[HttpGet]
		public dynamic Get()
		{
			return Ok(new[]
			{
				new
				{
					id = "123456",
					allowedUsers= 3,
					name= "3 usuários adicionais",
					price= "12345",
					startDate= "2017-11-06T00:00:00"
				},
				new
				{
					id = "3456",
					allowedUsers= 5,
					name= "5 usuários adicionais",
					price= "12345",
					startDate= "2017-11-06T00:00:00"
				},
				new
				{
					id = "8745",
					allowedUsers= 10,
					name= "10 usuários adicionais",
					price= "12345",
					startDate= "2017-11-06T00:00:00"
				}
			});
		}
	}
}