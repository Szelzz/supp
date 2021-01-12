﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Posts
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Body { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
