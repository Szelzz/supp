using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Posts;

namespace Supp.Web.Pages.Posts
{
    public class ListModel : PageModel
    {
        private readonly PostService postServce;

        public ListModel(PostService postServce)
        {
            this.postServce = postServce;
        }

        public IEnumerable<Post> Posts { get; set; }
        public int ProjectId { get; set; }

        public async Task OnGetAsync(int projectId)
        {
            ProjectId = projectId;
            Posts = await postServce.GetForProjectAsync(projectId);
        }
    }
}
