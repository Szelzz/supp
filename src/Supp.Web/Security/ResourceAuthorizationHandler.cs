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
    public class ResourceAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement, IResource>
    {
        private readonly PermissionAuthorizationService projectAuthorizationService;

        public ResourceAuthorizationHandler(PermissionAuthorizationService projectAuthorizationService)
        {
            this.projectAuthorizationService = projectAuthorizationService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, IResource resource)
        {
            if (projectAuthorizationService.Authorize(requirement.Permission, resource))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement, PermissionAuthorizationHandler.PermissionResourceHandler>
    {
        private readonly PermissionAuthorizationService projectAuthorizationService;

        public PermissionAuthorizationHandler(PermissionAuthorizationService projectAuthorizationService)
        {
            this.projectAuthorizationService = projectAuthorizationService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, PermissionResourceHandler resource)
        {
            if (projectAuthorizationService.Authorize(requirement.Permission))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        public static readonly PermissionResourceHandler Resource = new PermissionResourceHandler();
        public class PermissionResourceHandler { }

    }
}
