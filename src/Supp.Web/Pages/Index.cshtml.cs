using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Supp.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } = "Pagemodel";
        public void OnGet()
        {
            Message += $"Time: {DateTime.Now}";
        }
    }
}
