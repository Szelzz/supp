using Microsoft.EntityFrameworkCore;
using Supp.Core.Authorization;
using Supp.Core.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Projects
{
    public class ProjectService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly PermissionAuthorizationService authService;

        public ProjectService(ApplicationDbContext dbContext, PermissionAuthorizationService authService)
        {
            this.dbContext = dbContext;
            this.authService = authService;
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

        public async Task<List<Project>> GetAllAsync()
        {
            return await dbContext.Projects.Where(p => !p.Archived).ToListAsync();
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
