using Microsoft.AspNetCore.Identity;
using Supp.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Users
{
    public class UserRole
    {
        public int Id { get; set; }
        public Role Role { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int? ResourceId { get; set; }
    }
}
