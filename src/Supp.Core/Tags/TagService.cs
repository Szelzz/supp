using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<string> AddToProjectAsync(int projectId, string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                return null;

            var normalizedName = Regex.Replace(tagName.ToLower(), "[^a-z-]", "");
            if (dbContext.Tags.Any(t => t.ProjectId == projectId && t.Name == normalizedName))
                return null;

            if (normalizedName.Length < 3)
                return null;
            dbContext.Add(new Tag()
            {
                Name = normalizedName,
                ProjectId = projectId
            });
            await dbContext.SaveChangesAsync();
            return normalizedName;
        }

        public async Task RemoveTagAsync(int projectId, string tagName)
        {
            var tag = dbContext.Tags.FirstOrDefault(t => t.Name == tagName && t.ProjectId == projectId);
            if (tag == null)
                return;
            dbContext.Remove(tag);
            await dbContext.SaveChangesAsync();
        }
    }
}
