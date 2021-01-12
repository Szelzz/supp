using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Posts;

namespace Supp.Web.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly PostService postRepository;

        public CreateModel(PostService postRepository)
        {
            this.postRepository = postRepository;
        }

        [BindProperty]
        public Post Post { get; set; } = new Post();

        public void OnGet(int projectId)
        {
            Post.ProjectId = projectId;
        }

        public async Task<IActionResult> OnPostAsync(int projectId)
        {
            Post.ProjectId = projectId;

            if (!ModelState.IsValid)
                return Page();

            await postRepository.CreatePostAsync(Post);
            return RedirectToPage("Get", new { id = Post.Id });
        }
    }
}
