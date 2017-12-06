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
    public class UserController : Controller
    {
        //TODO Criado o UserController
        [HttpGet]
        public dynamic Get()
        {
            return Ok(new
            {
                id = "2g34h5j6k789l8kj765h4",
                accountId = 123,
                role = "ADMIN", // ADMIN | USER
                email = "luiz@gmail.com",
                name = "Luiz",
                surname = "Luiz",
                phone = "Luiz"
            });
        }

        [HttpPut("{id}")]
        public dynamic Put([FromRoute] int id, [FromBody] AddUserViewModel model)
        {
            return Ok(model);
        }
    }

    public class AddUserViewModel
    {
        public int Id { get; set; }
        public string accountId { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
    }
}