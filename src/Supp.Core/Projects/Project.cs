using Supp.Core.Authorization;
using Supp.Core.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Projects
{
    public class Project : IResource
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
            = new List<Post>();

        public int ProjectOptionsId { get; set; }
        public ProjectOptions ProjectOptions { get; set; } 
    }
}
