using Microsoft.AspNetCore.Authorization;
using Supp.Core.Authorization;

namespace Supp.Web.Security
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
