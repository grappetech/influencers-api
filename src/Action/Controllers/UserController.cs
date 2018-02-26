using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Action.Models;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Action.VewModels;

namespace Action.Controllers
{
    [Route("api/users")]
    [EnableCors("Default")]
    public class UserController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly UserManager<User> _manager;

        public UserController(HtmlEncoder htmlEncoder, UserManager<User> manager, ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
            _manager = manager;
        }

        [HttpGet("me")]
        public IActionResult Get()
        {
            return ValidateUser(() =>
            {
                try
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var param = new TokenValidationParameters
                    {
                        ValidIssuer = "http://nexo.ai",
                        ValidAudience = "https://api-act.mybluemix.net",
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kep2EpHUvA_eJAKaS$&G3-?E=#Efa5AX")),
                        ValidateLifetime = true
                    };

                    SecurityToken stoken;
                    ClaimsPrincipal claim = handler.ValidateToken(
                        HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower().Equals("authorization")).Value
                            .ToString().Replace("bearer ", ""), param, out stoken);
                    var user = _manager.FindByEmailAsync(claim.Claims
                        .FirstOrDefault(x => x.Type.ToLower().Contains("nameidentifier"))?.Value).Result;

                    if (user != null)
                    {
                        UserViewModel lUserViewModel = new UserViewModel
                        {
                            AccountId = user.AccountId.Value,
                            Email = user.Email,
                            Id = user.Id,
                            Name = user.Name,
                            Phone = user.PhoneNumber,
                            Administrator = (bool?)(user.Account?.Administrator == user) ?? false,
                            Role = user.Account != null && user.Account.Administrator != null &&
                                   user.Account.Administrator == user
                                ? "ADMIN"
                                : "USER",
                            SurName = user.Surname
                        };
                        return Ok(lUserViewModel);
                    }

                    return StatusCode((int) EServerError.BusinessError, new List<string> {"User not found."});
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            });
        }

        [HttpPut("{id}")]
        public dynamic Put([FromRoute] string id, [FromBody] AddUserViewModel model)
        {
            return ValidateUser(() =>
                {
                    try
                    {
                        if (_dbContext == null)
                            return NotFound("No database connection");
                        var data = _dbContext.Users.FirstOrDefault(x => x.Id.Equals(id));

                        if (data == null)
                            return StatusCode((int) EServerError.BusinessError,
                                new List<string> {"User not found with ID " + id.ToString() + "."});

                        data.Email = model.Email;
                        data.Name = model.Name;
                        data.Surname = model.Surname;
                        data.PhoneNumber = model.Phone;

                        _dbContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            );
        }
    }
}