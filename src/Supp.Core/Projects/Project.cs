using Supp.Core.Authorization;
using Supp.Core.Posts;
using Supp.Core.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
            = new List<Post>();

        public int ProjectOptionsId { get; set; }
        public ProjectOptions ProjectOptions { get; set; }

        public List<Tag> Tags { get; set; }
            = new List<Tag>();
    }
}
