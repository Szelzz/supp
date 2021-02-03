using Microsoft.AspNetCore.Authorization;
using Supp.Core.Authorization;

namespace Supp.Web.Security
{
    public class ProjectAuthorizationRequirement : IAuthorizationRequirement
    {
        public ProjectAuthorizationRequirement(ProjectOperation operation)
        {
            Operation = operation;
        }

        public ProjectOperation Operation { get; }
    }
}
