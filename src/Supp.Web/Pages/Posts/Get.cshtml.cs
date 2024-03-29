using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core;
using Supp.Core.Authorization;
using Supp.Core.Comments;
using Supp.Core.Modifier;
using Supp.Core.Posts;
using Supp.Core.Tags;
using Supp.Core.Voting;

namespace Supp.Web.Pages.Posts
{
    [IgnoreAntiforgeryToken(Order = 2000)] // Temporary
    public class GetModel : PageModel
    {
        private readonly PostService postService;
        private readonly TagService tagService;
        private readonly PermissionService permissionService;
        private readonly UniversalModelModifier modelModifier;
        private readonly VotingService votingService;
        private readonly CommentService commentService;

        public GetModel(PostService postService,
            TagService tagService,
            PermissionService permissionService,
            UniversalModelModifier modelModifier,
            VotingService votingService,
            CommentService commentService)
        {
            this.postService = postService;
            this.tagService = tagService;
            this.permissionService = permissionService;
            this.modelModifier = modelModifier;
            this.votingService = votingService;
            this.commentService = commentService;
        }

        public Post Post { get; set; }
        public List<Tag> AllowedTags { get; set; }
        public int Votes { get; set; }
        public bool UserVoted { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await postService.GetPostAsync(id);
            if (Post == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.PostCanRead, Post))
                return Unauthorized();

            AllowedTags = await tagService.GetForProjectAsync(Post.ProjectId);
            Votes = await votingService.CountVotesAsync(Post);
            UserVoted = await votingService.UserVoted(Post);

            return Page();
        }
        public async Task<IActionResult> OnPostEdit([FromBody] EditData model)
        {
            var post = await postService.GetPostAsync(model.ModelId);
            if (post == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.PostCanModify, post))
                return Unauthorized();

            var result = modelModifier.SetValue(post, model.PropertyName, model.Value);
            if (!result.Succeeded)
                return BadRequest();

            await postService.Edit(post);

            return new JsonResult(model.Value);
        }

        public async Task<IActionResult> OnPostVote(int postId)
        {
            var post = await postService.GetPostAsync(postId);
            if (post == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.PostCanVote, post))
                return Unauthorized();

            await votingService.VoteUpAsync(post);
            return new JsonResult("OK");
        }

        public async Task<IActionResult> OnPostUndoVote(int postId)
        {
            var post = await postService.GetPostAsync(postId);
            if (post == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.PostCanVote, post))
                return Unauthorized();

            await votingService.UndoAsync(post);
            return new JsonResult("OK");
        }

        public async Task<IActionResult> OnPostAllComments(int postId)
        {
            var post = await postService.GetPostAsync(postId);
            if (post == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.PostCanRead, post))
                return Unauthorized();

            var comments = await commentService.AllCommentsAsync(postId);
            return new AjaxResponse(comments.Select(c => new CommentModel(c)));
        }

        public async Task<IActionResult> OnPostNewComment([FromBody] CommentModel model)
        {
            var post = await postService.GetPostAsync(model.PostId);
            if (post == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.CommentCanAdd, post))
                return Unauthorized();

            var comment = await commentService.AddCommentAsync(model.PostId, model.Body);
            return new AjaxResponse(new CommentModel(comment));
        }

        public async Task<IActionResult> OnPostPinComment([FromBody] int commentId)
        {
            var comment = await commentService.GetCommentAsync(commentId);
            if (comment == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.CommentCanPin, comment))
                return Unauthorized();

            await commentService.PinComment(comment);
            return new JsonResult("Ok");
        }

        public async Task<IActionResult> OnPostUnpinComment([FromBody] int commentId)
        {
            var comment = await commentService.GetCommentAsync(commentId);
            if (comment == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.CommentCanPin, comment))
                return Unauthorized();

            await commentService.UnpinComment(comment);
            return new JsonResult("Ok");
        }
    }
}
