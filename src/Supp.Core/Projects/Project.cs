using Supp.Core.Posts;
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
        public List<Post> Posts { get; set; }
            = new List<Post>();
    }
}
