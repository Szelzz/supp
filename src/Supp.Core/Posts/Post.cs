using Supp.Core.Authorization;
using Supp.Core.Projects;
using Supp.Core.Tags;
using Supp.Core.Users;
using Supp.Core.Voting;
using System;
using System.Collections.Generic;

namespace Supp.Core.Posts
{
    public class Post : IProjectResource
    {
        public int Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
            = DateTimeOffset.Now;
        public string Title { get; set; }
        public string Body { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public User Author { get; set; }
        public string AuthorId { get; set; }

        public PostType Type { get; set; }
        public PostStatus Status { get; set; }
        public PostPriority Priority { get; set; }

        public ICollection<PostRelation> Parents { get; set; }
            = new List<PostRelation>();

        public ICollection<PostRelation> Children { get; set; }
            = new List<PostRelation>();

        public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();

        public ICollection<PostTag> Tags { get; set; }
            = new List<PostTag>();

        public ICollection<Vote> Votes { get; set; }
            = new List<Vote>();

        public string GetAuthorId() => AuthorId;

        public Project GetProject() => Project;
    }
}