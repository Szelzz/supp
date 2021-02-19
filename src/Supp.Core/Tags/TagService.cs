using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Tags
{
    public class TagService
    {
        private readonly ApplicationDbContext dbContext;

        public TagService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Tag>> GetForProjectAsync(int projectId)
        {
            return dbContext.Tags.Where(t => t.ProjectId == projectId).ToListAsync();
        }
    }
}
