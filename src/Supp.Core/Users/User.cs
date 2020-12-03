using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<ProjectRole> ProjectRole { get; set; }
            = new List<ProjectRole>();
    }
}