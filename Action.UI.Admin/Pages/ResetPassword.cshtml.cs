using Action.Data.Context;
using Action.Data.Models.Core;
using Action.Services.SMTP;
using ActionUI.Admin.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ActionUI.Admin.Pages
{
    public class ResetPasswordModel : PageModel
    {

        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;


        public ResetPasswordModel(UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;


        }

        public IActionResult OnPost()
        {
            object result = new { status=false };
            User user = _userManager.FindByNameAsync(this.UserLogin.Email).GetAwaiter().GetResult();
            if (user !=null)
            {

                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    result = new { status = false, message = "Não é possível atualizar a senha de usuário com senha definida." };
                }
                else
                {
                    var passAdded = _userManager.AddPasswordAsync(user, this.UserLogin.Password).GetAwaiter().GetResult();
                    if (passAdded.Succeeded)
                    {
                        result = new { status = true, message = "Senha atualizada com sucesso" };
                    }
                    else
                    {
                        result = new { status = false, message = "Falha na atualização da senha" };
                    }
                }

               
            }
            else
            {
                result = new { status = false, message = "Usuário não localizado" };
            }

            return new JsonResult(result);
        }

        [BindProperty]
        public LoginViewModel UserLogin { get; set; } = new LoginViewModel();
        public bool ShowFormChangePassword { get; set; }

        public void OnGet(string email, long t)
        {
            //t timetick

            DateTime dateLimit = new DateTime(t);
            DateTime now = DateTime.Now;
            
            //se a hora atual for maior que a data limite não prosseguir
            if( !(now > dateLimit))
            {
                User user = _userManager.FindByNameAsync(email).GetAwaiter().GetResult();
                if (user != null && string.IsNullOrEmpty(user.PasswordHash))
                {
                    this.ShowFormChangePassword = true;
                    this.UserLogin.Email = email;
                }
            }
        }
    }
}