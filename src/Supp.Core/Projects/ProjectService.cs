using Microsoft.AspNetCore.Identity;
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
    public class ProjectService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly PermissionService authService;
        private readonly UserManager<User> userManager;
        private readonly ClaimsPrincipal currentUser;
        private readonly PermissionService permissionService;

        public ProjectService(ApplicationDbContext dbContext,
            PermissionService authService,
            UserManager<User> userManager,
            ClaimsPrincipal currentUser,
            PermissionService permissionService)
        {
            this.dbContext = dbContext;
            this.authService = authService;
            this.userManager = userManager;
            this.currentUser = currentUser;
            this.permissionService = permissionService;
        }

        public async Task CreateAsync(Project project)
        {
            project.Id = 0;
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                dbContext.Projects.Add(project);
                await dbContext.SaveChangesAsync();
                await authService.GrantRoleAsync(Role.ProjectOwner, project);
                await transaction.CommitAsync();
            }
        }

        public async Task<List<Project>> GetAllForUserAsync()
        {
            var projects = await dbContext.Projects.Where(p => !p.Archived)
                .ToListAsync();
            return permissionService.AuthorizeCollection(Permission.ProjectCanRead, projects)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Task<Project> GetAsync(int projectId)
        {
            return dbContext.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task EditAsync(Project project)
        {
            if (project.Archived)
                throw new InvalidOperationException("Cannot edit archived project.");

            dbContext.Attach(project).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public Project GetWithPosts(int projectId)
        {
            return dbContext.Projects.Include(p => p.Posts)
                .FirstOrDefault(p => p.Id == projectId);
        }

        public async Task Archive(Project project)
        {
            project.Archived = true;
            dbContext.Attach(project).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
