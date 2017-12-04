using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Action.Filters;
using Action.Models;
using Action.Models.ServiceAccount;
using Action.Services.SMTP;
using Action.VewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Default")]
    public class AuthController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly ILogger<AuthController> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;
        private ApplicationDbContext _dbContext;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager
            , IPasswordHasher<User> passwordHasher, IConfigurationRoot configurationRoot,
            ILogger<AuthController> logger, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _configurationRoot = configurationRoot;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.Phone,
                PlanId = model.PlanId
            };
            var result = await _userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                Account account;
                if (model.AccountId == null)
                {
                    account = new Account
                    {
                        ActivationDate = DateTime.UtcNow,
                        AdministratorId = user.Id,
                        PlanId = user.PlanId,
                        Name = string.Join(user.Name, "_", user.Surname)
                    };
                    account.Users.Add(user);
                    _dbContext.Accounts.Add(account);
                }
                else
                {
                    account = _dbContext.Accounts.Find(model.AccountId.Value);
                    account.Users.Add(user);

                    _dbContext.Entry(account).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();

                var plan = _dbContext.Accounts.Find(user.AccountId);
                var userClaims = await _userManager.GetClaimsAsync(user);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("accountId", user.AccountId.ToString()),
                    new Claim("planId", plan.Id.ToString())
                }.Union(userClaims);

                var symmetricSecurityKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
                var signingCredentials =
                    new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    _configurationRoot["JwtSecurityToken:Issuer"],
                    _configurationRoot["JwtSecurityToken:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signingCredentials
                );
                return Ok(new
                {
                    accountId = user.AccountId,
                    planId = plan.PlanId,
                    user = model.Email,
                    token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expiration = jwtSecurityToken.ValidTo
                });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("error", error.Description);
            return BadRequest(result.Errors);
        }

        [ValidateForm]
        [AllowAnonymous]
        [HttpPost("login")]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                    return Unauthorized();
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) ==
                    PasswordVerificationResult.Success)
                {
                    var plan = _dbContext.Accounts.Find(user.AccountId);
                    var userClaims = await _userManager.GetClaimsAsync(user);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("accountId", user.AccountId.ToString()),
                        new Claim("planId", plan.Id.ToString())
                    }.Union(userClaims);

                    var symmetricSecurityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
                    var signingCredentials =
                        new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var jwtSecurityToken = new JwtSecurityToken(
                        _configurationRoot["JwtSecurityToken:Issuer"],
                        _configurationRoot["JwtSecurityToken:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signingCredentials
                    );
                    return Ok(new
                    {
                        planId = plan.PlanId,
                        user = model.Email,
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        expiration = jwtSecurityToken.ValidTo
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while creating token: {ex}");
                return StatusCode((int) HttpStatusCode.InternalServerError, "error while creating token");
            }
        }

        [HttpPost("")]
        [AllowAnonymous]
        [Route("requiretoken")]
        public async Task<IActionResult> RequireToken([FromBody] RequireAuthViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    var plan = _dbContext.Accounts.Find(user.AccountId);
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("accountId", user.AccountId.ToString()),
                        new Claim("planId", plan.Id.ToString())
                    }.Union(userClaims);


                    var symmetricSecurityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
                    var signingCredentials =
                        new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var jwtSecurityToken = new JwtSecurityToken(
                        _configurationRoot["JwtSecurityToken:Issuer"],
                        _configurationRoot["JwtSecurityToken:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signingCredentials
                    );
                    var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                    SmtpService.SendMessage(model.Email, "[ACTION-API Acesso]",
                        string.Concat(model.Url, "?token=", token, "&key=", user.AccountId));

                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while creating token: {ex}");
                return StatusCode((int) HttpStatusCode.InternalServerError, "error while creating token");
            }
        }


        [Authorize]
        [HttpGet("permitions/{id}")]
        public IActionResult Permitions(string id)
        {
            return Ok(JsonConvert.DeserializeObject<dynamic>(@"[
  {
    ""permissionId"": 1,
    ""name"": ""Usuario"",
    ""actions"": [
      {
        ""actionId"": 1,
        ""name"": ""Cadastrar"",
        ""letter"": ""C""
      },
      {
        ""actionId"": 2,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 3,
        ""name"": ""Excluir"",
        ""letter"": ""D""
      },
      {
        ""actionId"": 4,
        ""name"": ""Editar"",
        ""letter"": ""U""
      }
    ]
  },
  {
    ""permissionId"": 2,
    ""name"": ""Chamado"",
    ""actions"": [
      {
        ""actionId"": 6,
        ""name"": ""Acatar"",
        ""letter"": ""A""
      },
      {
        ""actionId"": 7,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 9,
        ""name"": ""Não Acatar"",
        ""letter"": ""N""
      }
    ]
  },
  {
    ""permissionId"": 3,
    ""name"": ""Dashboard"",
    ""actions"": [
      {
        ""actionId"": 12,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      }
    ]
  },
  {
    ""permissionId"": 4,
    ""name"": ""Polo de Manutenção"",
    ""actions"": [
      {
        ""actionId"": 16,
        ""name"": ""Cadastrar"",
        ""letter"": ""C""
      },
      {
        ""actionId"": 17,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 18,
        ""name"": ""Excluir"",
        ""letter"": ""D""
      },
      {
        ""actionId"": 19,
        ""name"": ""Editar"",
        ""letter"": ""U""
      }
    ]
  },
  {
    ""permissionId"": 5,
    ""name"": ""Cliente"",
    ""actions"": [
      {
        ""actionId"": 21,
        ""name"": ""Cadastrar"",
        ""letter"": ""C""
      },
      {
        ""actionId"": 22,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 23,
        ""name"": ""Excluir"",
        ""letter"": ""D""
      },
      {
        ""actionId"": 24,
        ""name"": ""Editar"",
        ""letter"": ""U""
      }
    ]
  },
  {
    ""permissionId"": 6,
    ""name"": ""Log"",
    ""actions"": [
      {
        ""actionId"": 26,
        ""name"": ""Cadastrar"",
        ""letter"": ""C""
      },
      {
        ""actionId"": 27,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 28,
        ""name"": ""Excluir"",
        ""letter"": ""D""
      },
      {
        ""actionId"": 29,
        ""name"": ""Editar"",
        ""letter"": ""U""
      }
    ]
  },
  {
    ""permissionId"": 7,
    ""name"": ""Mapa"",
    ""actions"": [
      {
        ""actionId"": 31,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      }
    ]
  },
  {
    ""permissionId"": 8,
    ""name"": ""Serviços"",
    ""actions"": [
      {
        ""actionId"": 32,
        ""name"": ""Cadastrar"",
        ""letter"": ""C""
      },
      {
        ""actionId"": 33,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 34,
        ""name"": ""Excluir"",
        ""letter"": ""D""
      },
      {
        ""actionId"": 35,
        ""name"": ""Editar"",
        ""letter"": ""U""
      }
    ]
  },
  {
    ""permissionId"": 9,
    ""name"": ""Mensagem"",
    ""actions"": [
      {
        ""actionId"": 37,
        ""name"": ""Enviar"",
        ""letter"": ""S""
      },
      {
        ""actionId"": 47,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      }
    ]
  },
  {
    ""permissionId"": 10,
    ""name"": ""Configuração"",
    ""actions"": [
      {
        ""actionId"": 42,
        ""name"": ""Cadastrar"",
        ""letter"": ""C""
      },
      {
        ""actionId"": 43,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 44,
        ""name"": ""Excluir"",
        ""letter"": ""D""
      },
      {
        ""actionId"": 45,
        ""name"": ""Editar"",
        ""letter"": ""U""
      }
    ]
  },
  {
    ""permissionId"": 11,
    ""name"": ""Configuração"",
    ""actions"": [
      {
        ""actionId"": 42,
        ""name"": ""Cadastrar"",
        ""letter"": ""C""
      },
      {
        ""actionId"": 43,
        ""name"": ""Visualizar"",
        ""letter"": ""R""
      },
      {
        ""actionId"": 44,
        ""name"": ""Excluir"",
        ""letter"": ""D""
      },
      {
        ""actionId"": 45,
        ""name"": ""Editar"",
        ""letter"": ""U""
      }
    ]
  }
]"));
        }
    }
}