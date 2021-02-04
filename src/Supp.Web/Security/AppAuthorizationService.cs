using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Supp.Core.Authorization;
using Supp.Core.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Web.Security
{
    public class AppAuthorizationService
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AppAuthorizationService(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            this.authorizationService = authorizationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<AuthorizationResult> AuthorizePermissionAsync(Permission permission)
        {
            return authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, PermissionAuthorizationHandler.Resource, new PermissionAuthorizationRequirement(permission));
        }

        public Task<AuthorizationResult> AuthorizePermissionAsync(Permission permission, IResource resource)
        {
            return authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, resource, new PermissionAuthorizationRequirement(permission));
        }
    }
}
