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
using Newtonsoft.Json;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Action.Controllers
{
    [Route("api/social-cities")]

    [EnableCors("Default")]
    [AllowAnonymous]
    public class SocialCityController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;
        
        public SocialCityController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }
        
        [HttpGet("entities/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return ValidateUser(()=>Ok(Mock()));
        }

        private List<CitySocialResultViewModel> Mock()
        {
            var json = System.IO.File.ReadAllText(Path.Combine(Startup.RootPath, "App_Data", "mock_cities.json"));
            var cities = JsonConvert.DeserializeObject<CidadeMock>(json);

            return (cities.cidades).Select(cidade => new CitySocialResultViewModel
            {
                Name = cidade,
                Score = cidade.Equals("São Paulo") ? Randomize.Next() : 0,
                State = "SP"
            }).OrderByDescending(x=>x.Score).ToList();
        }
    }
    
    internal class CidadeMock
    {
        public string sigla { get; set; }
        public string nome { get; set; }
        public List<string> cidades { get; set; }
    }
}