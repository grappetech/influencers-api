using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Action.Models;
using Action.Models.Watson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using Microsoft.EntityFrameworkCore;
using Action.Extensions;
using Action.VewModels;
using Action.Models.Plans;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Action.Services.SMTP;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Action.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("Default")]
	public class AccountController : BaseController
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;
		private readonly UserManager<User> _userManager;
		private readonly IConfigurationRoot _configurationRoot;

		public AccountController(UserManager<User> userManager, IConfigurationRoot configurationRoot,
			HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_userManager = userManager;
			_configurationRoot = configurationRoot;
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		// GET: api/values
		[HttpGet("{id}")]
		public IActionResult Get([FromRoute] int id)
		{
			return ValidateUser(() =>
			{
				try
				{
					if (_dbContext == null)
						return NotFound("No database connection");
					var data = _dbContext.Accounts.Include(x => x.Entities).Include(x => x.Administrator)
						.Include(x => x.Plan).ThenInclude(x => x.Features).Include(x => x.Users)
						.FirstOrDefault(x => x.Id == id);

					if (data == null)
						return StatusCode((int)EServerError.BusinessError,
							new List<string> { "Object not found with ID " + id.ToString() + "." });

					AccountViewModel lAccountViewModel = new AccountViewModel();
					lAccountViewModel.Id = data.Id;
					lAccountViewModel.Status = data.Status.ToString();
					lAccountViewModel.CompanyName = data.CompanyName ?? "";
					lAccountViewModel.Plan = new PlanViewModel
					{
						Id = data.Plan.Id,
						Name = data.Plan.Name,
						Price = data.Plan.Price,
						Slug = data.Plan.Slug,
					};
					data.Plan.Features.ForEach(x => lAccountViewModel.Plan.Features.Add(new FeatureViewModel
					{
						Description = x.Description
					}));

					data.SecondaryPlans.ForEach(x => lAccountViewModel.SecondaryPlans.Add(new SecondaryPlanViewModel
					{
						Account = lAccountViewModel,
						Id = x.Id,
						AllowedUsers = x.AllowedUsers,
						Name = x.Name,
						Price = x.Price,
						StartDate = x.StartDate
					}));

					data.Entities.ForEach(x => lAccountViewModel.Entities.Add(new EntityViewModel
					{
						Entity = x.Name,
						Id = x.Id,
						ImageUrl = x.PictureUrl,
						Type = x.Category
					}));

					data.Users.ForEach(x => lAccountViewModel.Users.Add(new UserViewModel
					{
						AccountId = x.AccountId.Value,
						Email = x.Email,
						Id = x.Id.ToString(),
						Name = x.Name,
						Phone = x.PhoneNumber,
						Role = x.Account != null && x.Account.Administrator != null && x.Account.Administrator == x
							? "ADMIN"
							: "USER",
						SurName = x.Surname,
					}));

					return Ok(lAccountViewModel);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			});
		}

		/*[HttpGet("{id}/payments")]
		public IActionResult GetPayments([FromRoute] long id)
		{
			return ValidateUser(() =>
			{
				var result = new List<PaymentHistoryViewModel>();
				result.Add(new PaymentHistoryViewModel
				{
					Id	= Guid.NewGuid().ToString(),
					ExpirationDSaDateTime = DateTime.Now.AddMonths(1).Date,
					PaymentDate = DateTime.Today.Date,
					PaymentDueDate = DateTime.Today.AddDays(-5),
					PaymentIdentifier = Guid.NewGuid().ToString(),
					PaymentType = "CREDIT CARD",
					PlanId = 1,
					SecondaryPlanId = 1
					
				});
				return Ok(result);
			});
		}*/

		[HttpPost("{id}/entities")]
		public IActionResult PostEntity([FromRoute] int id, [FromBody] EntityViewModel model)
		{
			return ValidateUser(() =>
			{
				if (ModelState.IsValid)
				{
					try
					{
						var account = _dbContext.Accounts.Include(x => x.Entities).FirstOrDefault(y => y.Id == id);
						if (account == null)
							throw new Exception("Account not found with ID: " + id);

						if (model.Id == 0)
						{
							var obj = new Entity
							{
								Name = model.Entity,
								CategoryId = Enum.Parse<ECategory>(model.Type),
								Date = DateTime.Today,
								//Alias = model.Alias,
								//FacebookUser = model.FacebookUser,
								//InstagranUser = model.InstagranUser,
								PictureUrl = model.ImageUrl,
								//SiteUrl = model.SiteUrl,
								//TweeterUser = model.TweeterUser,
								//YoutubeUser = model.YoutuberUser
							};
							_dbContext.Entities.Add(obj);
							account.Entities.Add(obj);
						}
						else
						{
							if (account.Entities == null)
								account.Entities = new List<Entity>();

							if (_dbContext.Entities.Find((long)model.Id) == null)
								throw new Exception("Entity not found with ID: " + model.Id);

							if (account.Entities.All(x => x.Id != model.Id))
								account.Entities.Add(_dbContext.Entities.Find((long)model.Id));
						}

						_dbContext.Entry(account).State = EntityState.Modified;
						_dbContext.SaveChanges();
						return Ok(account.Entities);
					}
					catch (Exception ex)
					{
						return StatusCode((int)EServerError.BusinessError, new List<string> { ex.Message });
					}
				}

				return StatusCode((int)EServerError.ModelIsNotValid, ModelState.GetErrors());
			});
		}

		[HttpDelete("{idaccount}/entities/{id}")]
		public IActionResult DeleteEntity([FromRoute] int idaccount, [FromRoute] long id)
		{
			return ValidateUser(() =>
			{
				try
				{
					if (_dbContext == null)
						return NotFound("No database connection");
					var data = _dbContext.Accounts.Include(x => x.Entities).FirstOrDefault(x => x.Id == idaccount);

					if (data == null)
						return StatusCode((int)EServerError.BusinessError,
							new List<string> { "Account not found with ID " + idaccount.ToString() + "." });

					var entity = data.Entities.FirstOrDefault(x => x.Id.Equals(id));

					if (entity == null)
						return StatusCode((int)EServerError.BusinessError,
							new List<string> { "Entity not found with ID " + id.ToString() + "." });

					data.Entities.Remove(entity);
					_dbContext.Entry(data).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return Ok();
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			});
		}

		[HttpPost("{id}/image")]
		public async Task<IActionResult> Post([FromRoute] int id, [FromForm] IFormFile file)
		{
			try
			{
				var fileId = Guid.NewGuid().ToString();

				if (file == null || file.Length == 0)
					return Content("file not selected");

				if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")))
					Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));

				var path = Path.Combine(
					Directory.GetCurrentDirectory(), "wwwroot/images",
					fileId + file.FileName.GetFileExtension());


				if (file == null || file.FileName.Equals(""))
					return Content("file not selected");

				if (_dbContext == null)
					return NotFound("No database connection");

				var data = _dbContext.Accounts.FirstOrDefault(x => x.Id == id);

				if (data == null)
					return StatusCode((int)EServerError.BusinessError,
						new List<string> { $"Account not found with ID {id}." });

				if (data.Plan != null && data.Plan.TypePlan != ETypePlan.Agency)
					return StatusCode((int)EServerError.BusinessError,
						new List<string> { "Imagem só é permitida para Agência." });
				using (var stream = new MemoryStream())
				{
					await file.CopyToAsync(stream);
					data.ImageUrl = ImageUpload.GenerateFileRoute(fileId + file.FileName.GetFileExtension(), stream,
						Request, _dbContext);
				}

				_dbContext.Entry(data).State = EntityState.Modified;
				_dbContext.SaveChanges();

				return Ok(new { ImageURL = data.ImageUrl });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("image/{filename}")]
		public async Task<IActionResult> GetImage([FromRoute] string filename)
		{
			var img = await _dbContext.Images.FirstOrDefaultAsync(x => x.ImageName.Equals(filename));
			var memory = new MemoryStream(Convert.FromBase64String(img.Base64Image)) { Position = 0 };
			return File(memory, filename.GetMimeType(), filename);
		}

		[HttpPost("{id}/users")]
		public async Task<IActionResult> PostUsers([FromRoute] int id, [FromBody] RequireAuthViewModel requireAuthView)
		{
			try
			{
				if (requireAuthView.Email == null || requireAuthView.Email.Equals(""))
					return StatusCode((int)EServerError.BusinessError, new List<string> { "E-mail não informado." });
				if (requireAuthView.Url == null || requireAuthView.Url.Equals(""))
					return StatusCode((int)EServerError.BusinessError, new List<string> { "URL não informado." });

				if (_dbContext == null)
					return NotFound("No database connection");
				var data = _dbContext.Accounts.Include(x => x.Users).FirstOrDefault(x => x.Id.Equals(id));

				if (data == null)
					return StatusCode((int)EServerError.BusinessError,
						new List<string> { "Account not found with ID " + id.ToString() + "." });

				var user = data.Users.FirstOrDefault(x => x.Email.Equals(requireAuthView.Email));
				if (user != null)
					return StatusCode((int)EServerError.BusinessError,
						new List<string> { "Usuário já cadastrado com este e-mail para esta Conta." });

				user = new User
				{
					UserName = requireAuthView.Email,
					Email = requireAuthView.Email,
					Name = "",
					Surname = "",
					PhoneNumber = "",
					PlanId = data.PlanId,
					AccountId = data.Id
				};
				var result = await _userManager.CreateAsync(user, Randomize.NewPassword(8));

				if (!result.Succeeded)
					throw new Exception("Falha ao criar usuário.\n" +
										result.Errors.Select(x => x.Description).StringJoin(", "));

				var userClaims = await _userManager.GetClaimsAsync(user);
				var plan = _dbContext.Accounts.Find(user.AccountId);
				var expiration = DateTime.UtcNow.AddDays(1);
				var claims = new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim("accountId", user.AccountId.ToString()),
					new Claim("planId", plan.Id.ToString()),
					new Claim(".expires", expiration.Ticks.ToString())
				}.Union(userClaims);
				var symmetricSecurityKey =
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
				var signingCredentials =
					new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

				var jwtSecurityToken = new JwtSecurityToken(
					_configurationRoot["JwtSecurityToken:Issuer"],
					_configurationRoot["JwtSecurityToken:Audience"],
					claims,
					expires: expiration,
					signingCredentials: signingCredentials
				);
				var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

				SmtpService.SendMessage(user.Email, "[ACTION-API Acesso]",
					string.Concat(requireAuthView.Url, "?token=", token));

				return Ok(new
				{
					email = user.Email,
					url = requireAuthView.Url
				});
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//TODO Adicionado delete
		[HttpDelete("{idaccount}/users/{id}")]
		public IActionResult Delete([FromRoute] int idaccount, [FromRoute] string id)
		{
			return ValidateUser(() =>
			{
				try
				{
					if (_dbContext == null)
						return NotFound("No database connection");
					var data = _dbContext.Accounts.Include(x => x.Users).FirstOrDefault(x => x.Id == idaccount);

					if (data == null)
						return StatusCode((int)EServerError.BusinessError,
							new List<string> { "Account not found with ID " + idaccount.ToString() + "." });

					var user = data.Users.FirstOrDefault(x => x.Id.Equals(id));

					if (user == null)
						return StatusCode((int)EServerError.BusinessError,
							new List<string> { "User not found with ID " + id.ToString() + "." });

					if (user.Account != null && user.Account.Administrator == user)
						return StatusCode((int)EServerError.BusinessError,
							new List<string> { "Usuário Administrador não pode ser excluído." });

					data.Users.Remove(user);
					_dbContext.Entry(data).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return Ok();
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			});
		}

		//TODO: Adicionado SecondaryPlan
		[HttpPost("{id}/secondaryplans")]
		public IActionResult PostSecondaryPlans([FromRoute] int id, [FromBody] SecondaryPlanViewModel secondaryPlanView)
		{
			return ValidateUser(() =>
			{
				try
				{
					var account = _dbContext.Accounts.Include(x => x.SecondaryPlans).FirstOrDefault(y => y.Id == id);
					if (account == null)
						throw new Exception("Account not found with ID: " + id);

					var secondaryPlan = account.SecondaryPlans.FirstOrDefault(x => x.Id == secondaryPlanView.Id);
					if (secondaryPlan != null)
						throw new Exception("SecondaryPlan already associated with the Account.");

					SecondaryPlan newSecondaryPlan = new SecondaryPlan
					{
						Account = account,
						AccountId = account.Id,
						AllowedUsers = secondaryPlanView.AllowedUsers,
						Name = secondaryPlanView.Name,
						Price = secondaryPlanView.Price,
						StartDate = secondaryPlanView.StartDate
					};

					account.SecondaryPlans.Add(newSecondaryPlan);
					_dbContext.Entry(account).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return Ok(newSecondaryPlan);
				}
				catch (Exception ex)
				{
					return StatusCode((int)EServerError.BusinessError, new List<string> { ex.Message });
				}
			});
		}


		[HttpGet("cancela/{id}")]
		public IActionResult GetCancela([FromRoute] int id)
		{
			return ValidateUser(() => Ok(new
			{
				highPrice = true,
				platformNotUseful = true,
				doesntMatchMyNeeds = true,
				unableToUseThePlatformEffectively = true,
				message = "message message message message message "
			})); 
		}

		[HttpPost("cancela/{id}")]
		public IActionResult PostCancela([FromRoute] int id)
		{
			return ValidateUser(() => Ok());
		}

		[HttpGet("{id}/payments")]
		public IActionResult GetPayments([FromRoute] int id)
		{
			try
			{
				return ValidateUser(() => Ok(new[]
				{
					new
					{
						id = 123456,
						plan = new
						{
							id = 2,
							name = "MARCA",
							slug = "marca",
							features = new[]
							{
								new {description = "Monitore a relevância digital da sua marca ou de seu produto"},
								new {description = "Cadastre até 3 usuários para o monitoramento"},
								new {description = "Encontre as celebridades perfeitas para as suas campanhas"}
							},
							price = 2000
						},
						secondaryPlans = new[]
						{
							new
							{
								id = "123456",
								allowedUsers = 3,
								name = "3 usuários adicionais",
								price = "12345",
								startDate = "2017-11-06T00:00:00"
							}
						},
						expirationdate = "2017-11-06T00:00:00",
						status = "PAID" // PENDING | PAID
					}
				}));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}