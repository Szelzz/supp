using Microsoft.EntityFrameworkCore;
using Supp.Core.Authorization;
using Supp.Core.Data.EF;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Projects
{
    public class ProjectPermissionService
    {
        private readonly ClaimsPrincipal currentUser;
        private readonly ApplicationDbContext dbContext;
        private readonly PermissionService permissionService;

        public ProjectPermissionService(ClaimsPrincipal currentUser, ApplicationDbContext dbContext, PermissionService permissionService)
        {
            this.currentUser = currentUser;
            this.dbContext = dbContext;
            this.permissionService = permissionService;
        }

        public async Task AddRoleAsync(int projectId, Role role, string username)
        {
            var project = dbContext.Projects.Find(projectId);
            var user = dbContext.Users.FirstOrDefault(u => u.UserName == username);

            if (project == null || user == null)
                return;

            await permissionService.GrantRoleForUserAsync(user, role, project);
        }

        public async Task RemoveRoleAsync(int projectId, Role role, string username)
        {
            var project = dbContext.Projects.Find(projectId);
            var user = dbContext.Users.FirstOrDefault(u => u.UserName == username);

            if (project == null || user == null)
                return;

            if (user.UserName == currentUser.Identity.Name)
                throw new Exception("Nie można usunąć uprawnienia dotyczącego ciebie");

            await permissionService.RemoveRoleFromUserAsync(user, role, project);
        }

        public Task<List<UserRole>> GetRolesForProjectAsync(int projectId)
        {
            return dbContext.UserRoles
                .Include(r => r.User)
                .Where(r => r.ResourceId == projectId)
                .ToListAsync();
        }
    }
}
