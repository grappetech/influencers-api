using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Action.Extensions;
using Action.Models;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Action.Controllers
{
    [Route("api/briefings/entities")]

    [EnableCors("Default")]
    [AllowAnonymous]
    public class BriefingCotroller : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;

        public BriefingCotroller(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }
                     
        
        [HttpGet("{entityId}")]
        public IActionResult Get([FromRoute] long entityId)
        {
            return ValidateUser(() =>
            {
                try
                {
                    if (_dbContext == null)
                        return NotFound("No database connection");
                    var data = _dbContext.Briefings.Where(x => x.EntityId == entityId).ToList();

                    List<BriefingViewModel> briefings = new List<BriefingViewModel>();
                    if (data.Count > 0)
                        foreach (var d in data)
                            briefings.Add(new BriefingViewModel
                            {
                                Id = d.Id,
                                Entity = d.Name,
                                AgeRange = d.AgeRange,
                                Description = d.Description,
                                City = d.City,
                                DocumentUrl = d.DocumentUrl,
                                Factor = d.Factor,
                                Gender = d.Gender,
                                Name = d.Name,
                                Personality = d.Personality,
                                State = d.State,
                                Strength = d.Strength,
                                Tone = d.Tone,
                                Value = d.Value
                            });
                    return Ok(briefings);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }   
            });
        }
        
        [HttpGet("{entityId}/{id}")]
        public IActionResult GetById([FromRoute]long entityId, [FromRoute]long id)
        {
            return ValidateUser(() =>
            {
                try
                {
                    if (_dbContext == null)
                        return NotFound("No database connection");
                    var data = _dbContext.Briefings.Where(x => x.EntityId == entityId).ToList();

                    List<BriefingViewModel> briefings = new List<BriefingViewModel>();
                    if (data.Count > 0)
                        foreach (var d in data)
                            briefings.Add(new BriefingViewModel
                            {
                                Id = d.Id,
                                Entity = d.Name,
                                AgeRange = d.AgeRange,
                                Description = d.Description,
                                City = d.City,
                                DocumentUrl = d.DocumentUrl,
                                Factor = d.Factor,
                                Gender = d.Gender,
                                Name = d.Name,
                                Personality = d.Personality,
                                State = d.State,
                                Strength = d.Strength,
                                Tone = d.Tone,
                                Value = d.Value
                            });
                    return Ok(briefings.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }   
            });
        }
        
        [HttpPost("{entityId}")]
        public IActionResult Post([FromRoute]int entityId,[FromBody] BriefingViewModel model)
        {
            if (ModelState.IsValid)
                return ValidateUser(() =>
                {
                    if(model.Strength <=0)
                    model.Strength =  (decimal)(model.Description.Length / 1200.0);
                    try
                    {
                        var briefing = new Briefing
                        {
                            AgeRange = model.AgeRange,
                            Analysis = string.Empty,
                            Brand = string.Empty,
                            City = string.Empty,
                            Description = model.Description,
                            DocumentUrl = model.DocumentUrl,
                            Entity = model.Entity,
                            EntityId = entityId,
                            Factor = model.Factor,
                            Gender = model.Gender,
                            Name = model.Name,
                            Personality = model.Personality,
                            State = model.State,
                            Tone = model.Tone,
                            Value = model.Value,
                            Strength = model.Strength
                        };
                        _dbContext.Briefings.Add(briefing);
                        _dbContext.SaveChanges();
                        model.Id = briefing.Id;
                        return Ok(model);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new {ex.Message});
                    }
                });
            return ValidateUser(() => BadRequest(new {Message = "Modelo inválido."}));
        }
        
        [HttpPost("{entityId}/upload")]
        public async Task<IActionResult> Post([FromRoute] int entityId, [FromForm] IFormFile file)
        {
            try
            {
                var fileId = Guid.NewGuid().ToString();

                if (file == null || file.Length == 0)
                    return Content("file not selected");

                if (file == null || file.FileName.Equals(""))
                    return Content("file not selected");

                if (_dbContext == null)
                    return NotFound("No database connection");

                var data = _dbContext.Entities.FirstOrDefault(x => x.Id == entityId);

                if (data == null)
                    return StatusCode((int) EServerError.BusinessError,
                        new List<string> {$"Entity not found with ID {entityId}."});

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    data.PictureUrl = ImageUpload.GenerateFileRoute(fileId + file.FileName.GetFileExtension(), stream,
                        Request, _dbContext);
                }

                _dbContext.Entry(data).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return Ok(new {documentUrl = data.PictureUrl});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{entityId}/{id}/recomendation")]
        public IActionResult GetRecomendations([FromRoute]long entityId, [FromRoute]long id)
        {
            return ValidateUser(() =>
            {
                try
                {
                    if (_dbContext == null)
                        return NotFound("No database connection");
                    var data = _dbContext.Entities.Where(x => x.Id != entityId).Take(8).ToList();
var random = new Random(Randomize.Next());
                    List<EntityRecomendationViewModel> entities = new List<EntityRecomendationViewModel>();
                    if (data.Count > 0)
                        foreach (var d in data)
                            entities.Add(new EntityRecomendationViewModel
                            {
                                Id = d.Id,
                                Entity = d.Name,
                                ImageUrl = d.PictureUrl,
                                Type = d.Category,
                                Score = random.Next(50,100)/100 
                            });
                    return Ok(entities.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }   
            });
        }
    }
}