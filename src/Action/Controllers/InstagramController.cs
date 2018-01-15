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
            return ValidateUser(()=>Ok(Mock()));
        }

        private InstagramResultViewModel Mock()
        {
            return new InstagramResultViewModel
            {
                AgeRanges = new AgeRangesViewModel(),
                Stats = new []
                {
                    new SocialStatViewModel
                    {
                        Month = DateTime.Today.AddMonths(-5).ToString("MM-yyyy")
                    },
                    new SocialStatViewModel
                    {
                        Month = DateTime.Today.AddMonths(-4).ToString("MM-yyyy")
                    },
                    new SocialStatViewModel
                    {
                        Month = DateTime.Today.AddMonths(-3).ToString("MM-yyyy")
                    },
                    new SocialStatViewModel
                    {
                        Month = DateTime.Today.AddMonths(-2).ToString("MM-yyyy")
                    },
                    new SocialStatViewModel
                    {
                        Month = DateTime.Today.AddMonths(-1).ToString("MM-yyyy")
                    },
                    new SocialStatViewModel
                    {
                        Month = DateTime.Today.ToString("MM-yyyy")
                    }
                }.ToList()
            };
        }
    }
}