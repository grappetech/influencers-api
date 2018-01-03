using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Encodings.Web;
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
	[Route("api/entities")]

	[EnableCors("Default")]
	[AllowAnonymous]
	public class EntityController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly HtmlEncoder _htmlEncoder;

		public EntityController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
		{
			_dbContext = dbContext;
			_htmlEncoder = htmlEncoder;
		}

		// GET: api/values
		[HttpGet("")]
		public dynamic Get([FromQuery] string name = "")
		{
			try
			{
				if (_dbContext == null)
					return NotFound("No database connection");
				var data = _dbContext.Entities.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

				List<EntityViewModel> lEntityViewModel = new List<EntityViewModel>();
				if (data != null)
					foreach (var d in data)
						lEntityViewModel.Add(new EntityViewModel
						{
							Id = d.Id,
							Entity = d.Name,
							Type = d.Category,
							ImageUrl = d.PictureUrl,
						});
				return lEntityViewModel;
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public dynamic GetById(long id)
		{
			try
			{
				if (_dbContext == null)
					return NotFound("No database connection");
				var data = _dbContext.Entities.FirstOrDefault(x => x.Id == id);

				if (data == null)
					return StatusCode((int)EServerError.BusinessError, new List<string> { "Object not found with ID " + id.ToString() + "." });

				return new EntityViewModel
				{
					Id = data.Id,
					Entity = data.Name,
					Type = data.Category,
					ImageUrl = data.PictureUrl
				};
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPost("{id}/image")]
		public dynamic PostImage([FromRoute]long id, IFormFile file)
		{

			try
			{
				if (_dbContext == null)
					return NotFound("No database connection");
				var data = _dbContext.Entities.FirstOrDefault(x => x.Id == id);

				if (file == null || file.Length == 0)
					return Content("file not selected");

				var path = Path.Combine(
					Directory.GetCurrentDirectory(), "wwwroot",
					file.FileName);

				using (var stream = new FileStream(path, FileMode.Create))
				{
					file.CopyTo(stream);
				}

				return Ok(Request.Path.Value + "/" + file.FileName.Replace(".", "___"));

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}/image/{filename}")]
		public dynamic GetImage([FromRoute]long id, [FromRoute] string filename)
		{

			var path = Path.Combine(
				Directory.GetCurrentDirectory(), "wwwroot",
				filename.Replace("___", "."));

			if (System.IO.File.Exists(path))
			{
				var bytes = System.IO.File.ReadAllBytes(path);
				var result_ = new HttpResponseMessage(HttpStatusCode.OK);
				result_.Content = new ByteArrayContent(bytes);
				result_.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
				return result_;
			}
			else
			{
				return NotFound();
			}

		}


		// POST api/values
		[HttpPost]
		public dynamic Post([FromBody]  Entity model)
		{
			try
			{
				if (_dbContext == null)
				{
					return NotFound("No database connection");
				}

				var dt = _dbContext.Entities.FirstOrDefault(x => x.Name.Equals(model.Name));

				if (dt != null)
					return new EntityViewModel
					{
						Id = dt.Id,
						Entity = dt.Name,
						Type = dt.Category,
						ImageUrl = dt.PictureUrl
					};

				var entity = new Entity
				{
					Alias = model.Alias,
					CategoryId = ECategory.Brand,
					Date = DateTime.Today,
					Name = model.Name,
					FacebookUser = model.FacebookUser,
					InstagranUser = model.InstagranUser,
					PictureUrl = model.PictureUrl,
					SiteUrl = model.SiteUrl,
					TweeterUser = model.TweeterUser,
					YoutubeUser = model.YoutubeUser
				};

				var data = _dbContext.Entities.Add(entity);
				_dbContext.SaveChanges();

				return new EntityViewModel
				{
					Id = data.Entity.Id,
					Entity = data.Entity.Name,
					Type = data.Entity.Category,
					ImageUrl = data.Entity.PictureUrl
				};
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		// POST api/values
		[HttpPut]
		public dynamic Put([FromBody] Entity entity)
		{
			try
			{
				if (_dbContext == null)
				{
					return NotFound("No database connection");
				}
				var data = _dbContext.Entry(entity).State = EntityState.Modified;
				_dbContext.SaveChanges();
				return data;
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


	}
}