using Supp.Core.Posts;
using System;

namespace Supp.Core.ExternalSources
{
    public class ExternalPost
    {
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public Post ToPost()
        {
            return new Post()
            {
                Type = PostType.Task,
                Status = PostStatus.New,
                CreationDate = CreationDate,
                Title = Title,
                Body = Body,
            };
        }
    }
}
