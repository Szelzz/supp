using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Projects;

namespace Supp.Web.Pages.Home
{
    public class LoggedModel : PageModel
    {
        private readonly ProjectService projectService;

        public LoggedModel(ProjectService projectService)
        {
            this.projectService = projectService;
        }

        public List<Project> Projects { get; set; }

        public async Task OnGetAsync()
        {
            Projects = await projectService.GetAllForUserAsync();
        }
    }
}
