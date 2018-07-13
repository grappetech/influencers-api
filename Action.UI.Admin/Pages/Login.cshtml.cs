using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action.Data.Models.Core;
using Action.Services.SMTP;
using ActionUI.Admin.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ActionUI.Admin.Pages
{
    public class LoginModel : PageModel
    {



        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfigurationRoot _configurationRoot;

        public LoginModel(UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IConfigurationRoot configurationRoot)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _configurationRoot = configurationRoot;

        }


        [BindProperty]
        public LoginViewModel UserLogin { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            User user = _userManager.FindByNameAsync(this.UserLogin.Email).GetAwaiter().GetResult();
           
            var userFound = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, this.UserLogin.Password) == PasswordVerificationResult.Success;


            if (userFound)
            {
                HttpContext.Session.SetString(Constants.USER_SESSION, user.Id);
                return RedirectToPage("Logged/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnGetResetPassword(string email)
        {
            object result = new { status=false };

            User user = _userManager.FindByNameAsync(email).GetAwaiter().GetResult();

            try
            {
                if (user != null)
                {
                 var removePasswordResult = _userManager.RemovePasswordAsync(user).GetAwaiter().GetResult();
                 if (removePasswordResult.Succeeded)                 
                    {

                        var dateTimeLimit = DateTime.Now.AddMinutes(30);
                        long dateLimitTick = dateTimeLimit.Ticks;
                        string baseUrl = _configurationRoot["BaseUrl"];
                        string link = $"{baseUrl}/ResetPassWord?&email={email}&t={dateLimitTick}";
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<p>Admin ACT10N<br> Segue abaixo link para Reset de senha<br></p>");
                        sb.Append($"<a href='{link}'>Reset de Senha Admin ACT10N</a>");
                        sb.Append($"<br>Este link é válido até {string.Format("{0:HH:mm} do dia {1:dd/MM/yyyy}", dateTimeLimit, dateTimeLimit)}");

                        result = new { status = true, message = "E-mail de recuperação de senha enviado com sucesso" };

                        SmtpService.SendMessage(email, "Reset de senha Admin Action", sb.ToString());
                    }

                    
                }
                else
                {
                    result = new { status = false, message = "Login inválido" };
                }
            }
            catch (Exception ex)
            {

                result = new { status = false, message = ex.Message};
            }



            return new JsonResult(result);
        }



    }
}