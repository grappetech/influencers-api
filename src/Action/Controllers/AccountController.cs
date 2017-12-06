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

namespace Action.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;

        public AccountController(HtmlEncoder htmlEncoder, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        // GET: api/values
        [HttpGet("{id}")]
        public dynamic Get([FromRoute] int id)
        {
            try
            {
                if (_dbContext == null)
                    return NotFound("No database connection");
                var data = _dbContext.Accounts.Include(x => x.Administrator).Include(x => x.Plan).ThenInclude(x => x.Features).Include(x => x.Users).FirstOrDefault(x => x.Id == id);

                //TODO: Alterado o Retorno
                return Ok(new
                {
                    data.Id,
                    data.Plan,
                    secondaryPlans = new[]
                    {
                        new
                        {
                            id= "123456",
                            allowedUsers= 3,
                            name= "3 usuários adicionais",
                            price= "12345",
                            startDate= "2017-11-06T00:00:00"
                        }
                    },
                    entities = new List<Entity>(),
                    users = new[]
                    {
                        new
                        {
                            id= "2g34h5j6k789l8kj765h4",
                            accountId= 123,
                            role= "ADMIN", // ADMIN | USER
                            email= "luiz@gmail.com",
                            name= "Luiz",
                            surname= "Luiz",
                            phone= "Luiz"
                        }
                    },
                    status = "ACTIVE"
                    //name = data.Administrator?.Name,
                    //email = data.Administrator?.Email,

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/entities")]
        public dynamic Post([FromRoute] int id, [FromBody] List<AddEntityViewModel> model)
        {
            var account = _dbContext.Accounts.Find(id);
            foreach (var entity in model)
            {
                if (entity.Id == 0)
                {
                    var obj = new Entity
                    {
                        Name = entity.Entity,
                        CategoryId = Enum.Parse<ECategory>(entity.Type)
                    };
                    _dbContext.Entities.Add(obj);
                    account.Entities.Add(obj);

                }
                else
                {
                    if (account.Entities.All(x => x.Id != entity.Id))
                        account.Entities.Add(_dbContext.Entities.Find(entity.Id));
                }
            }
            _dbContext.Entry(account).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return account.Entities;
        }

        //TODO: Adicionado Dois Retornos Mocados em AccountController
        [HttpPost("{id}/image")]
        public dynamic Post([FromRoute] int id)
        {
            return "https://cdn1.iconfinder.com/data/icons/social-messaging-productivity-1-1/128/gender-male2-512.png";
        }

        [HttpPost("{id}/users")]
        public dynamic PostUsers([FromRoute] int id)
        {
            return Ok(new
            {
                email = "luiz@nexo.ai",
                url = "http://localhost:3000/#/auth"
            });
        }

        //TODO Adicionado delete
        [HttpDelete("{idaccount}/users/{id}")]
        public dynamic Delete([FromRoute] int idaccount, [FromRoute] int id)
        {
            return Ok();
        }

        //TODO: Adicionado SecondaryPlan
        [HttpPost("{id}/secondaryplans")]
        public dynamic PostSecondaryPlans([FromRoute] int id)
        {
            return Ok(new
            {
                id = "8745",
                allowedUsers = 10,
                name = "10 usuários adicionais",
                price = "12345",
                startDate = "2017-11-06T00:00:00"
            });
        }

        [HttpPost("cancela/{id}")]
        public dynamic PostCancela([FromRoute] int id)
        {
            return Ok(new
            {
                highPrice = true,
                platformNotUseful = true,
                doesntMatchMyNeeds = true,
                unableToUseThePlatformEffectively = true,
                message = "message message message message message "
            });
        }

        [HttpGet("{id}/payments")]
        public dynamic GetPayments([FromRoute] int id)
        {
            return Ok(new[]
            {
                new
                {
                    id= 123456,
                    plan=new
                    {
                        id= 2,
                        name= "MARCA",
                        slug= "marca",
                        features= new[]
                        {
                            new { description= "Monitore a relevância digital da sua marca ou de seu produto"},
                            new { description= "Cadastre até 3 usuários para o monitoramento"},
                            new { description= "Encontre as celebridades perfeitas para as suas campanhas"}
                        },
                        price= 2000
                        },
                        secondaryPlans= new []
                        {
                            new
                            {
                                id= "123456",
                                allowedUsers= 3,
                                name= "3 usuários adicionais",
                                price= "12345",
                                startDate= "2017-11-06T00:00:00"
                            }
                        },
                        expirationdate= "2017-11-06T00:00:00",
                        status= "PENDING" // PENDING | PAID
                 }
            });
        }
    }
    public class AddEntityViewModel
    {
        //TODO: Adicionado as Propriedades imageUrl e industryId
        public int Id { get; set; }
        public string Entity { get; set; }
        public string Type { get; set; }
        public string imageUrl { get; set; }
        public int industryId { get; set; }
    }
}