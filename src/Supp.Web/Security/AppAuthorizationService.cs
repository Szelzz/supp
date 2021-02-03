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

        public Task<AuthorizationResult> AuthorizeProjectAsync(Project project, ProjectOperation projectOperation)
        {
            return authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, project, new ProjectAuthorizationRequirement(projectOperation));
        }
    }
}
