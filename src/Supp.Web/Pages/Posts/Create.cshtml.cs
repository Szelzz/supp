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
        public Post Post { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await postRepository.CreatePostAsync(Post);
            return RedirectToPage("Get", new { id = Post.Id });
        }
    }
}
