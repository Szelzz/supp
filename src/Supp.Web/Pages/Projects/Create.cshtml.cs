using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Projects;

namespace Supp.Web.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly ProjectService projectService;

        public CreateModel(ProjectService projectService)
        {
            this.projectService = projectService;
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Project.ProjectOptions = new ProjectOptions();
            await projectService.CreateAsync(Project);
            return RedirectToPage("/Posts/Index", new { projectId = Project.Id });
        }
    }
}
