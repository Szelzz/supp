using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using Supp.Core.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Search
{
    public class SearchService
    {
        private readonly ApplicationDbContext dbContext;

        public SearchService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Post>> SearchAsync(SearchQuery searchQuery)
        {
            // Tags

            var query = dbContext.Posts
                .Where(p =>
                    p.ProjectId == searchQuery.ProjectId);
            if (!string.IsNullOrEmpty(searchQuery.Text))
            {
                var searchWords = searchQuery.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                var searchText = "";
                var tags = new List<string>();
                foreach (var word in searchWords)
                {
                    if (word.StartsWith('#') && word.Length > 1)
                    {
                        tags.Add(word[1..]);
                    }
                    else
                    {
                        searchText += " " + word;
                    }
                }
                searchText = searchText.Trim();
                if (searchText.Length > 0)
                {
                    query = query.Where(p =>
                        p.Title.Contains(searchText) ||
                        p.Body.Contains(searchText));
                }

                if (tags.Count > 0)
                    query = query.Where(p => p.Tags.Any(t => tags.Contains(t.Tag.Name)));
            }

            query = query.Where(p =>
                searchQuery.Statuses.Contains(p.Status));
            query = query.Where(p =>
                searchQuery.Priorities.Contains(p.Priority));
            query = query.Where(p =>
                searchQuery.Types.Contains(p.Type));

            query = query.OrderByDescending(p => p.CreationDate);
            return query.ToListAsync();
        }
    }

    public class SearchQuery
    {
        public int ProjectId { get; set; }

        public string Text { get; set; }
        public List<PostStatus> Statuses { get; set; }
        public List<PostType> Types { get; set; }
        public List<PostPriority> Priorities { get; set; }
    }
}
