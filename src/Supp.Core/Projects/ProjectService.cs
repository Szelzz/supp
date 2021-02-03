using Microsoft.EntityFrameworkCore;
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

        public ProjectService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Project project)
        {
            project.Id = 0;
            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await dbContext.Projects.Where(p => !p.Archived).ToListAsync();
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
