using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Projects;

namespace Supp.Web.Pages.Projects
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProjectService projectService;
        private readonly IAuthorizationService authorizationService;

        public IndexModel(ProjectService projectService, IAuthorizationService authorizationService)
        {
            this.projectService = projectService;
            this.authorizationService = authorizationService;
        }

        public List<Project> Projects { get; set; }

        public async Task OnGetAsync()
        {
            Projects = await projectService.GetAllAsync();
        }
    }
}
