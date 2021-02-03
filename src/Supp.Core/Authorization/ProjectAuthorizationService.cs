using Supp.Core.Projects;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Authorization
{
    public class ProjectAuthorizationService
    {
        private readonly ClaimsPrincipal user;

        public ProjectAuthorizationService(ClaimsPrincipal user)
        {
            this.user = user;
        }

        //public bool CanRead(Project project)
        //{
        //    return
        //        user.HasClaim(ProjectClaim.IsClaim(project.Id, ProjectRoleType.Owner))
        //        || user.HasClaim(ProjectClaim.IsClaim(project.Id, ProjectRoleType.Contributor));
        //}

        //public bool CanEdit(Project project)
        //{
        //    return user.HasClaim(ProjectClaim.IsClaim(project.Id, ProjectRoleType.Owner));
        //}

        public bool Authorize(Project project, ProjectOperation operations)
        {
            if (project == null)
                return false;

            return operations switch
            {
                ProjectOperation.Read => user.HasClaim(ProjectClaim.IsClaim(project.Id, ProjectRoleType.Owner))
                                       || user.HasClaim(ProjectClaim.IsClaim(project.Id, ProjectRoleType.Contributor)),
                ProjectOperation.Edit => user.HasClaim(ProjectClaim.IsClaim(project.Id, ProjectRoleType.Owner)),
                _ => false,
            };
        }
    }
}
