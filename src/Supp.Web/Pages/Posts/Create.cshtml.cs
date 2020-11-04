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
        private readonly IPostRepository postRepository;

        public CreateModel(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [BindProperty]
        public Post Post { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            postRepository.Create(Post);
            return RedirectToPage("Get", new { id = Post.Id });
        }
    }
}
