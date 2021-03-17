using Supp.Core.Posts;
using System;

namespace Supp.Web.Pages.Posts
{
    public class PostListItem
    {
        public PostListItem(Post post, int votes)
        {
            Id = post.Id;
            CreationDate = post.CreationDate;
            Title = post.Title;
            Type = post.Type;
            Status = post.Status;
            Priority = post.Priority;
            Votes = votes;
        }

        public int Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Title { get; set; }

        public PostType Type { get; set; }
        public PostStatus Status { get; set; }
        public PostPriority Priority { get; set; }

        public int Votes { get; set; }
    }
}
