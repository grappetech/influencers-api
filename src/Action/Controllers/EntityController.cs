using System;
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
                var data = _dbContext.Entities.Where(x=>x.Name.ToLower().Contains(name.ToLower())).ToList();
                return
                    from d in data select new {d.Id, imageUrl = "https://cdn1.iconfinder.com/data/icons/social-messaging-productivity-1-1/128/gender-male2-512.png", type = d.Category, entity = d.Name};
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
                return new {data.Id, imageUrl = data.PictureUrl, type = data.Category, entity = data.Alias};
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

                return Ok(Request.Path.Value + "/" + file.FileName.Replace(".","___"));

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
                    filename.Replace("___","."));
            
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
        public dynamic Post([FromBody]  EntityCreateViewModel model)
        {
            try
            {
                if (_dbContext == null)
                {
                    return NotFound("No database connection");
                }
                
                var entity = new Entity
                {
                    Alias = model.Entity,
                    CategoryId = ECategory.Brand,
                    Date = DateTime.Today,
                    Name = model.Entity
                    
                };
                var data = _dbContext.Entities.Add(entity);
                _dbContext.SaveChanges();
                return data;
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