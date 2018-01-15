using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Action.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ValidateUser(Func<IActionResult> action)
        {
            if (!HttpContext.Request.Headers.Any(x => x.Key.ToLower().Equals("authorization")))
                return NotAuthorized("Token de Autenticação não encontrado");

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var param = new TokenValidationParameters
            {
                ValidIssuer = "http://nexo.ai",
                ValidAudience = "https://api-act.mybluemix.net",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kep2EpHUvA_eJAKaS$&G3-?E=#Efa5AX")),
                ValidateLifetime = true
            };

            SecurityToken stoken;
            ClaimsPrincipal claim = handler.ValidateToken(
                HttpContext.Request.Headers.FirstOrDefault(x => x.Key.ToLower().Equals("authorization")).Value
                    .ToString().Replace("bearer ", ""), param, out stoken);

            var validate = claim.Claims.FirstOrDefault(x => x.Type.ToLower().Contains("exp")).Value;
            long ticks;
    
            if(!long.TryParse(validate, out ticks))
                return NotAuthorized("Token inválido.");
            
            var date = new DateTime(ticks);
            
            if (date < DateTime.UtcNow)
                return NotAuthorized("Token expirado.");
            
            return action.Invoke();
        }

        private IActionResult NotAuthorized(string message = "Acesso negado.")

        {
            return StatusCode(401, new { message });
        }
    }
}