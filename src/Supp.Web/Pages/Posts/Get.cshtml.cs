using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Posts;

namespace Supp.Web.Pages.Posts
{
    public class GetModel : PageModel
    {
        private readonly PostService postService;

        public GetModel(PostService postService)
        {
            this.postService = postService;
        }

        public Post Post { get; set; }

        public async Task OnGetAsync(int id)
        {
            Post = await postService.GetPostAsync(id);
        }

        public async Task<IActionResult> OnPostComment(int postId, Comment comment)
        {
            comment.Id = 0;
            await postService.AddCommentAsync(postId, comment);
            return RedirectToPage("Get", new { id = postId });
        }
    }
}
