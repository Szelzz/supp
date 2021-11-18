using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Posts
{
    public class PostService
    {
        private readonly ClaimsPrincipal user;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public PostService(ClaimsPrincipal user, ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.user = user;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task CreatePostAsync(Post post)
        {
            post.Id = 0;
            post.Author = null;
            post.AuthorId = userManager.GetUserId(user);
            dbContext.Add(post);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await dbContext.Posts.ToListAsync();
        }

        public async Task AddCommentAsync(int postId, Comment comment)
        {
            comment.CreateTime = DateTime.Now;
            comment.PostId = postId;
            dbContext.Add(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetForProjectAsync(int projectId)
        {
            return await dbContext.Posts.Where(p => p.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<Post> GetPostAsync(int postId)
        {
            return await dbContext.Posts
                .Include(p => p.Comments)
                .Include(p => p.Project)
                .Include(p => p.Tags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task Edit(Post post)
        {
            if (post.Status == PostStatus.Removed)
                throw new InvalidOperationException("Cannot edit archived post.");

            dbContext.Attach(post).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task ArchivePost(Post post)
        {
            post.Status = PostStatus.Removed;
            dbContext.Attach(post);
            await dbContext.SaveChangesAsync();
        }

        public async Task JoinPostAsync(Post post, List<int> sourcesIds)
        {
            post.Id = 0;

            var sources = new List<Post>();

            using var transaction = dbContext.Database.BeginTransaction();

            dbContext.Posts.Add(post);
            await dbContext.SaveChangesAsync();
            foreach (var sourceId in sourcesIds)
            {
                var source = dbContext.Posts.Find(sourceId);
                if (source == null)
                    continue;

                source.Status = PostStatus.Removed;
                sources.Add(source);
                var relation = new PostRelation(sourceId, post.Id);
                dbContext.PostRelations.Add(relation);
            }
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task SplitTaskAsync(int sourceId, List<Post> posts)
        {
            var source = await dbContext.Posts.FindAsync(sourceId);

            if (source == null)
                throw new InvalidOperationException("Source doesn't exists");

            source.Status = PostStatus.Removed;

            using var transaction = dbContext.Database.BeginTransaction();
            foreach (var post in posts)
            {
                post.Id = 0;
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();
                var relation = new PostRelation(sourceId, post.Id);
                dbContext.PostRelations.Add(relation);
            }
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}
