using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Authorization;
using Supp.Core.Posts;
using Supp.Core.Projects;
using Supp.Web.Security;

namespace Supp.Web.Pages.Posts
{
    public class ListModel : PageModel
    {
        private readonly PostService postServce;
        private readonly ProjectService projectService;
        private readonly AppAuthorizationService authorizationService;

        public ListModel(PostService postServce, ProjectService projectService, AppAuthorizationService authorizationService)
        {
            this.postServce = postServce;
            this.projectService = projectService;
            this.authorizationService = authorizationService;
        }

        public IEnumerable<Post> Posts { get; set; }
        public int ProjectId { get; set; }

        public async Task<IActionResult> OnGetAsync(int projectId)
        {
            ProjectId = projectId;
            var project = projectService.GetWithPosts(ProjectId);
            var result = await authorizationService.AuthorizePermissionAsync(Permission.ProjectCanRead, project);
            if (!result.Succeeded)
                return new ForbidResult();

            Posts = await postServce.GetForProjectAsync(projectId);
            return Page();
        }
    }
}
