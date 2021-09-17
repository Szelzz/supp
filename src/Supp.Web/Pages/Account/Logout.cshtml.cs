using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Supp.Web.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public LogoutModel()
        {
        }

        public async Task<ActionResult> OnGet()
        {
            await this.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToPage("/Index");
        }
    }
}
