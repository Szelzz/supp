using Microsoft.Extensions.Logging;
using Supp.Core.Posts;
using Supp.Core.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.ExternalSources
{
    public class ExternalSourcesTask : ITask
    {
        private readonly ILogger logger;
        private readonly IEnumerable<IExternalSource> externalSources;
        private readonly PostService postService;

        public ExternalSourcesTask(ILogger logger,
            IEnumerable<IExternalSource> externalSources,
            PostService postService)
        {
            this.logger = logger;
            this.externalSources = externalSources;
            this.postService = postService;
        }

        public TimeSpan Interval => TimeSpan.FromMinutes(1);

        public string Id => "ExternalSources";

        public void Execute()
        {
            foreach (var externalSource in externalSources)
            {
                try
                {
                    ParseNew(externalSource.GetNew());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while parsing externalSource: " + externalSource.Id);
                }
            }
        }

        private void ParseNew(IEnumerable<ExternalPost> externalPosts)
        {
            foreach (var post in externalPosts)
            {
                postService.CreatePostAsync(post.ToPost()).Wait();
            }
        }
    }
}
