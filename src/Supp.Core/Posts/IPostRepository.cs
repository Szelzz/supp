using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Posts
{
    public interface IPostRepository
    {
        Post Get(int postId);
        IEnumerable<Post> GetAll();
        void Create(Post post);
    }

    public class InMemoryPostRepository : IPostRepository
    {
        private static readonly List<Post> posts = new List<Post>()
        {
            new Post()
            {
                Id = 1,
                CreationDate = DateTimeOffset.Now,
                Title = "Post 1",
                Body = "Body of post 1",
            },
            new Post()
            {
                Id = 2,
                CreationDate = DateTimeOffset.Now,
                Title = "Post 2",
                Body = "Body of post 2",
            },
        };

        public IEnumerable<Post> GetAll()
        {
            return posts.ToList();
        }

        public Post Get(int postId)
        {
            return posts.FirstOrDefault(p => p.Id == postId);
        }

        public void Create(Post post)
        {
            post.Id = GetLastId();
            post.CreationDate = DateTimeOffset.UtcNow;
            posts.Add(post);
        }

        private int GetLastId()
        {
            return (posts.OrderByDescending(d => d.Id).FirstOrDefault()?.Id ?? 0) + 1;
        }
    }
}
