using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Posts
{
    public class PostService
    {
        private readonly ApplicationDbContext dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreatePostAsync(Post post)
        {
            post.Id = 0;
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
            return await dbContext.Posts.Include(p => p.Comments).
                FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task Edit(Post post)
        {
            if (post.Archived)
                throw new InvalidOperationException("Cannot edit archived post.");

            dbContext.Attach(post).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task ArchivePost(Post post)
        {
            post.Archived = true;
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

                source.Archived = true;
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

            source.Archived = true;

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
