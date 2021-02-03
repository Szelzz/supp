using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Supp.Core.Authorization;
using Supp.Core.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Web.Security
{
    public class ProjectAuthorizationHandler : AuthorizationHandler<ProjectAuthorizationRequirement, Project>
    {
        private readonly ProjectAuthorizationService projectAuthorizationService;

        public ProjectAuthorizationHandler(ProjectAuthorizationService projectAuthorizationService)
        {
            this.projectAuthorizationService = projectAuthorizationService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectAuthorizationRequirement requirement, Project resource)
        {
            if (projectAuthorizationService.Authorize(resource, requirement.Operation))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
