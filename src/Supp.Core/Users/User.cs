using Microsoft.AspNetCore.Identity;
using Supp.Core.Voting;
using System.Collections.Generic;

namespace Supp.Core.Users
{
    public class User : IdentityUser
    {
        public ICollection<UserRole> Roles { get; set; }
            = new List<UserRole>();

        public ICollection<Vote> Votes { get; set; }
            = new List<Vote>();
    }
}