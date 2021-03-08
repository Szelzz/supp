using Supp.Core.Posts;
using System.Collections.Generic;

namespace Supp.Core.Search
{
    public class SearchData
    {
        public static IEnumerable<PostStatus> PostStatusForSearch { get; }
            = new List<PostStatus>()
            {
                PostStatus.New,
                PostStatus.Assigned,
                PostStatus.Done,
                PostStatus.Incorrect,
                PostStatus.Unworkable,
            };
    }
}
