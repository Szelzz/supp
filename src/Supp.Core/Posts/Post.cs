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

        public List<PostRelation> Parents { get; set; }
            = new List<PostRelation>();

        public List<PostRelation> Children { get; set; }
            = new List<PostRelation>();
        public bool Archived { get; set; }

    }
}