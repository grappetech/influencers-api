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
    [Route("api/social-state")]

    [EnableCors("Default")]
    [AllowAnonymous]
    public class SocialStateController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;
        
        public SocialStateController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }
        
        [HttpGet("entities/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return ValidateUser(()=>Ok(Mock()));
        }

        private List<StateSocialResultViewModel> Mock()
        {
            return new[]
            {
                new StateSocialResultViewModel
                {
                    Name = "Acre"
                },
                new StateSocialResultViewModel
                {
                    Name = "Alagoas"
                },
                new StateSocialResultViewModel
                {
                    Name = "Amapá"
                },
                new StateSocialResultViewModel
                {
                    Name = "Amazonas"
                },
                new StateSocialResultViewModel
                {
                    Name = "Bahia"
                },
                new StateSocialResultViewModel
                {
                    Name = "Ceará"
                },
                new StateSocialResultViewModel
                {
                    Name = "Distrito Federal"
                },
                new StateSocialResultViewModel
                {
                    Name = "Espírito Santo"
                },
                new StateSocialResultViewModel
                {
                    Name = "Goiás"
                },
                new StateSocialResultViewModel
                {
                    Name = "Maranhão"
                },
                new StateSocialResultViewModel
                {
                    Name = "Mato Grosso"
                },
                new StateSocialResultViewModel
                {
                    Name = "Mato Grosso do Sul"
                },
                new StateSocialResultViewModel
                {
                    Name = "Minas Gerais"
                },
                new StateSocialResultViewModel
                {
                    Name = "Pará"
                },
                new StateSocialResultViewModel
                {
                    Name = "Paraíba"
                },
                new StateSocialResultViewModel
                {
                    Name = "Paraná"
                },
                new StateSocialResultViewModel
                {
                    Name = "Pernambuco"
                },
                new StateSocialResultViewModel
                {
                    Name = "Piauí"
                },
                new StateSocialResultViewModel
                {
                    Name = "Rio de Janeiro"
                },
                new StateSocialResultViewModel
                {
                    Name = "Rio Grande do Norte"
                },
                new StateSocialResultViewModel
                {
                    Name = "Rio Grande do Sul"
                },
                new StateSocialResultViewModel
                {
                    Name = "Rondônia"
                },
                new StateSocialResultViewModel
                {
                    Name = "Roraima"
                },
                new StateSocialResultViewModel
                {
                    Name = "Santa Catarina"
                },
                new StateSocialResultViewModel
                {
                    Name = "São Paulo"
                },
                new StateSocialResultViewModel
                {
                    Name = "Sergipe"
                },
                new StateSocialResultViewModel
                {
                    Name = "Tocantins"
                }
            }.ToList();
        }
    }
}