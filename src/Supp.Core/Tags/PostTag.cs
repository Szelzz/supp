using Supp.Core.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Tags
{
    public class PostTag
    {
        public int TagId { get; set; }
        public int PostId { get; set; }
        public Tag Tag { get; set; }
        public Post Post { get; set; }
    }
}
