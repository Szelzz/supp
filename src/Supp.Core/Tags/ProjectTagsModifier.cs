using Supp.Core.Data.EF;
using Supp.Core.Modifier;
using Supp.Core.Projects;
using Supp.Core.Tags;
using System;
using System.Linq;
using System.Reflection;

namespace Supp.Core.Tags
{
    public class ProjectTagsModifier : IModelModifier
    {
        private readonly ApplicationDbContext dbContext;

        public ProjectTagsModifier(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string PropertyName => nameof(Project.Tags);
        public Type ModelType => typeof(Project);
        public Type PropertyType => null;

        public void SetValue(object model, PropertyInfo property, string propertyValue)
        {
            var project = (Project)model;
            if (propertyValue == null)
                propertyValue = "";

            var currentTags = dbContext.Tags.Where(t => t.ProjectId == project.Id);
            var tagsNames = propertyValue.Split(",").Select(Tag.NormalizeName).ToArray();

            // remove deleted tags
            foreach (var currentTag in currentTags)
            {
                if (!tagsNames.Contains(currentTag.Name))
                {
                    dbContext.Remove(currentTag);
                }
            }

            // add new tags
            foreach (var tagName in tagsNames)
            {
                if (!currentTags.Any(t => t.Name == tagName))
                {
                    var tag = new Tag()
                    {
                        Name = tagName,
                        ProjectId = project.Id
                    };
                    dbContext.Add(tag);
                }
            }

            dbContext.SaveChanges();
        }
    }
}
