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
    public class IndustriesController : Controller
    {
        [HttpGet]
        public dynamic Get()
        {
            return new[]
            {
                new
                {
                    id = 1,
                    name = "Celebridade"
                },
                new
                {
                    id = 2,
                    name = "Automobilistica"
                },
                new
                {
                    id = 3,
                    name = "qwertyui"
                }
            };
        }
    }
}