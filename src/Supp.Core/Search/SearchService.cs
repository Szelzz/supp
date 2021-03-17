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
            var query = dbContext.Posts
                .Where(p =>
                    p.ProjectId == searchQuery.ProjectId);
            if (!string.IsNullOrEmpty(searchQuery.Text))
            {
                query = query.Where(p =>
                    p.Title.Contains(searchQuery.Text) ||
                    p.Body.Contains(searchQuery.Text));
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
