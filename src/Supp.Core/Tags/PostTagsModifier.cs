using Supp.Core.Data.EF;
using Supp.Core.Modifier;
using Supp.Core.Posts;
using System;
using System.Linq;
using System.Reflection;

namespace Supp.Core.Tags
{
    public class PostTagsModifier : IModelModifier
    {
        private readonly ApplicationDbContext dbContext;

        public PostTagsModifier(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string PropertyName => nameof(Post.Tags);
        public Type ModelType => typeof(Post);
        public Type PropertyType => null;

        public void SetValue(object model, PropertyInfo property, string propertyValue)
        {
            var post = (Post)model;
            if (propertyValue == null)
                propertyValue = "";

            var tagsNames = propertyValue.Split(",").Select(Tag.NormalizeName).ToArray();
            var newTags = dbContext.Tags.Where(t => tagsNames.Contains(t.Name)).ToList();

            var currentTags = dbContext.PostTag.Where(t => t.PostId == post.Id).ToList();

            // remove deleted tags
            foreach (var currentTag in currentTags)
            {
                if (!newTags.Any(t => t.Id == currentTag.TagId))
                    dbContext.Remove(currentTag);
            }

            // add new tags
            foreach (var newTag in newTags)
            {
                if (!currentTags.Any(t => t.TagId == newTag.Id))
                {
                    var postTag = new PostTag()
                    {
                        PostId = post.Id,
                        TagId = newTag.Id
                    };
                    dbContext.Add(postTag);
                }
            }

            dbContext.SaveChanges();
        }
    }
}
