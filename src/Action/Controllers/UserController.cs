using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Action.Models;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Action.VewModels;

namespace Action.Controllers
{
	[Route("api/users")]
	[EnableCors("Default")]
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;
		private readonly UserManager<User> _manager;

		public UserController(HtmlEncoder htmlEncoder, UserManager<User> manager, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
			_manager = manager;
		}

		[HttpGet("me")]
		public async Task<IActionResult> Get()
		{
			try
			{
				JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
				var param = new TokenValidationParameters
				{
					ValidIssuer = "http://nexo.ai",
					ValidAudience = "https://api-act.mybluemix.net",
					ValidateIssuerSigningKey = true,
					IssuerSigningKey =
							new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kep2EpHUvA_eJAKaS$&G3-?E=#Efa5AX")),
					ValidateLifetime = true
				};

				SecurityToken stoken;
				ClaimsPrincipal claim = handler.ValidateToken(HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower().Equals("authorization")).Value.ToString().Replace("bearer ", ""), param, out stoken);
				var user = await _manager.FindByEmailAsync(claim.Claims.FirstOrDefault(x => x.Type.ToLower().Contains("nameidentifier")).Value);

				if (user != null)
				{
					UserViewModel lUserViewModel = new UserViewModel
					{
						AccountId = user.AccountId.Value,
						Email = user.Email,
						Id = user.Id,
						Name = user.Name,
						Phone = user.PhoneNumber,
						Role = user.Account != null && user.Account.Administrator != null && user.Account.Administrator == user ? "ADMIN" : "USER",
						SurName = user.Surname
					};
					return Ok(lUserViewModel);
				}
				else
					return StatusCode((int)EServerError.BusinessError, new List<string> { "User not found." });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public dynamic Put([FromRoute] string id, [FromBody] AddUserViewModel model)
		{
			try
			{
				if (_dbContext == null)
					return NotFound("No database connection");
				var data = _dbContext.Users.FirstOrDefault(x => x.Id.Equals(id));

				if (data == null)
					return StatusCode((int)EServerError.BusinessError, new List<string> { "User not found with ID " + id.ToString() + "." });

				data.Email = model.email;
				data.Name = model.name;
				data.Surname = model.surname;
				data.PhoneNumber = model.phone;

				_dbContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				_dbContext.SaveChanges();

				return Ok(model);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}

	public class AddUserViewModel
	{
		public string Id { get; set; }
		public int accountId { get; set; }
		public string role { get; set; }
		public string email { get; set; }
		public string name { get; set; }
		public string surname { get; set; }
		public string phone { get; set; }
	}
}