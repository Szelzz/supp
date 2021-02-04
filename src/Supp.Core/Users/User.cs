using Microsoft.AspNetCore.Identity;
using Supp.Core.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Users
{
    public class User : IdentityUser
    {
        public List<UserRole> Roles { get; set; }
            = new List<UserRole>();
    }
}