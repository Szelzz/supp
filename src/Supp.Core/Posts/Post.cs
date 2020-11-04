using System;

namespace Supp.Core.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
            = DateTimeOffset.Now;
        public string Title { get; set; }
        public string Body { get; set; }
    }
}