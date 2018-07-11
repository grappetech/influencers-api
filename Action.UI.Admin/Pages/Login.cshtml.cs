using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Data.Models.Core;
using ActionUI.Admin.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActionUI.Admin.Pages
{
    public class LoginModel : PageModel
    {


        //private readonly RoleManager<Role> _roleManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        
        public LoginModel(UserManager<User> userManager,IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            
        }


        [BindProperty]
        public LoginViewModel UserLogin { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {

            User user = await _userManager.FindByNameAsync(this.UserLogin.Email);
            //if (user == null)
            //{
            //    //return Unauthorized();
            //}
            var userFound = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, this.UserLogin.Password) == PasswordVerificationResult.Success;

            if (userFound)
            {
                HttpContext.Session.SetString(Constants.USER_SESSION, user.Id);
                return RedirectToPage("Logged/Index");
            }

            return Page();
        }
    }
}