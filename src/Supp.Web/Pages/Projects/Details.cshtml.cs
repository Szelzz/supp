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
            Project = await projectService.GetAsync(model.ModelId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            var result = modelModifier.SetValue(Project, model.PropertyName, model.Value);
            if (!result.Succeeded)
                return BadRequest();

            await projectService.EditAsync(Project);
            return new JsonResult(model.Value);
        }

        public async Task<IActionResult> OnPostTagAdd([FromBody] TagInfo tagInfo)
        {
            Project = await projectService.GetAsync(tagInfo.ProjectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            var resultTag = await tagService.AddToProjectAsync(tagInfo.ProjectId, tagInfo.TagName);
            if (resultTag == null)
                return new JsonResult("Invalid name")
                {
                    StatusCode = 400
                };

            return new JsonResult(resultTag);
        }

        public async Task<IActionResult> OnPostTagRemove([FromBody] TagInfo tagInfo)
        {
            Project = await projectService.GetAsync(tagInfo.ProjectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();


            await tagService.RemoveTag(tagInfo.ProjectId, tagInfo.TagName);

            return new JsonResult("Ok");
        }
        public class TagInfo
        {
            public int ProjectId { get; set; }
            public string TagName { get; set; }
        }
    }
}
