using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Supp.Core;
using Supp.Core.Authorization;
using Supp.Core.Data.EF;
using Supp.Core.Modifier;
using Supp.Core.Projects;
using Supp.Core.Tags;

namespace Supp.Web.Pages.Projects
{
    [IgnoreAntiforgeryToken(Order = 2000)] // Temporary
    public class DetailsModel : PageModel
    {
        private readonly ProjectService projectService;
        private readonly UniversalModelModifier modelModifier;
        private readonly TagService tagService;
        private readonly PermissionService permissionService;

        public DetailsModel(ProjectService projectService,
            UniversalModelModifier modelModifier,
            TagService tagService,
            PermissionService permissionService)
        {
            this.projectService = projectService;
            this.modelModifier = modelModifier;
            this.tagService = tagService;
            this.permissionService = permissionService;
        }

        public Project Project { get; set; }
        public string ProjectTags { get; set; }

        public async Task<IActionResult> OnGet(int projectId)
        {
            Project = await projectService.GetAsync(projectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            ProjectTags = string.Join(",", (await tagService.GetForProjectAsync(projectId)).Select(t => t.Name));

            return Page();
        }

        public async Task<IActionResult> OnPostEdit([FromBody] EditData model)
        {
            var project = await projectService.GetAsync(model.ModelId);
            if (project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            var result = modelModifier.SetValue(project, model.PropertyName, model.Value);
            if (!result.Succeeded)
                return BadRequest();

            await projectService.EditAsync(project);
            return new JsonResult(model.Value);
        }
    }
}
