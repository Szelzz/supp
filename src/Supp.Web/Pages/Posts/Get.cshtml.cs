using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core;
using Supp.Core.Modifier;
using Supp.Core.Posts;
using Supp.Web.Security;

namespace Supp.Web.Pages.Posts
{
    [IgnoreAntiforgeryToken(Order = 2000)] // Temporary
    public class GetModel : PageModel
    {
        private readonly PostService postService;
        private readonly AppAuthorizationService authorizationService;
        private readonly UniversalModelModifier modelModifier;

        public GetModel(PostService postService, AppAuthorizationService authorizationService, UniversalModelModifier modelModifier)
        {
            this.postService = postService;
            this.authorizationService = authorizationService;
            this.modelModifier = modelModifier;
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

        public async Task<IActionResult> OnPostEdit([FromBody] EditData model)
        {
            var post = await postService.GetPostAsync(model.ModelId);
            if (post == null)
                return NotFound();

            var result = modelModifier.SetValue(post, model.PropertyName, model.Value);
            if (!result.Succeeded)
                return BadRequest();

            await postService.Edit(post);
            return new JsonResult(model.Value);
        }
    }
}
