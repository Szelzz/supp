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
using Supp.Core.Search;
using Supp.Core.Voting;

namespace Supp.Web.Pages.Posts
{
    public class ListModel : PageModel
    {
        private readonly PostService postServce;
        private readonly ProjectService projectService;
        private readonly PermissionService permissionService;
        private readonly VotingService votingService;
        private readonly SearchService searchService;

        public ListModel(PostService postServce,
            ProjectService projectService,
            PermissionService permissionService,
            VotingService votingService,
            SearchService searchService)
        {
            this.postServce = postServce;
            this.projectService = projectService;
            this.permissionService = permissionService;
            this.votingService = votingService;
            this.searchService = searchService;
        }

        public IEnumerable<Post> Posts { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int projectId)
        {
            ProjectId = projectId;
            Project = projectService.GetWithPosts(ProjectId);
            var result = permissionService.Authorize(Permission.ProjectCanRead, Project);
            if (!result)
                return new ForbidResult();

            Posts = await postServce.GetForProjectAsync(projectId);
            return Page();
        }

        public async Task<IActionResult> OnPostSearchAsync([FromBody] SearchQuery searchQuery)
        {
            var project = projectService.GetWithPosts(searchQuery.ProjectId);
            var result = permissionService.Authorize(Permission.ProjectCanRead, project);
            if (!result)
                return new ForbidResult();

            var posts = await searchService.SearchAsync(searchQuery);

            return new AjaxResponse(posts.Select(p => new PostListItem(p, votingService.CountVotesAsync(p).Result)));
        }

        public Task<int> VotesAsync(Post post)
        {
            return votingService.CountVotesAsync(post);
        }

    }
}
