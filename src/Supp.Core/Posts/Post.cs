using Supp.Core.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Supp.Core.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
            = DateTimeOffset.Now;
        public string Title { get; set; }
        public string Body { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public PostType Type { get; set; }
        public PostStatus Status { get; set; }
        public PostPriority Priority { get; set; }

        public List<PostRelation> Parents { get; set; }
            = new List<PostRelation>();

        public List<PostRelation> Children { get; set; }
            = new List<PostRelation>();

        public List<Comment> Comments { get; set; }
            = new List<Comment>();
    }
}