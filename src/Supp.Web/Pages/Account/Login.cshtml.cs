using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Users;

namespace Supp.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [BindProperty]
        [Required]
        public string Password { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await signInManager.PasswordSignInAsync(UserName, Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (returnUrl == null)
                    return RedirectToPage("/Projects/Index");
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
