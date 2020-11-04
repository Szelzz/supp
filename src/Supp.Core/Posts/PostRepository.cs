using Supp.Core.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext dbContext;

        public PostRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Post post)
        {
            post.Id = 0;
            dbContext.Add(post);
            dbContext.SaveChanges();
        }

        public Post Get(int postId)
        {
            return dbContext.Posts.Find(postId);
        }

        public IEnumerable<Post> GetAll()
        {
            return dbContext.Posts.ToList();
        }
    }
}
