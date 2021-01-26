using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Users
{
    public class User : IdentityUser
    {
        public List<ProjectRole> ProjectRole { get; set; }
            = new List<ProjectRole>();
    }
}