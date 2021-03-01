using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using Supp.Core.Posts;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Comments
{
    public class CommentService
    {
        private readonly ClaimsPrincipal currentUser;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public CommentService(ClaimsPrincipal currentUser, ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.currentUser = currentUser;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<Comment> AddCommentAsync(int postId, string body)
        {
            var comment = new Comment()
            {
                AuthorId = userManager.GetUserId(currentUser),
                Body = body,
                PostId = postId,
                CreateTime = DateTime.Now,
                Pinned = false
            };
            dbContext.Add(comment);
            await dbContext.SaveChangesAsync();
            await dbContext.Entry(comment).Reference(c => c.Author).LoadAsync();
            return comment;
        }

        public async Task PinComment(Comment comment)
        {
            comment.Pinned = true;
            dbContext.Attach(comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public Task<List<Comment>> AllCommentsAsync(int postId)
        {
            var comments = dbContext.Comments
                .Include(c => c.Author)
                .Where(c => c.PostId == postId)
                .OrderBy(c => c.Pinned)
                .ThenBy(c => c.CreateTime)
                .ToListAsync();

            return comments;
        }
    }
}
