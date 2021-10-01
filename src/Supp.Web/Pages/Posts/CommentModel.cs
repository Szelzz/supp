using Supp.Core.Posts;

namespace Supp.Web.Pages.Posts
{
    public class CommentModel
    {
        public CommentModel() { }
        public CommentModel(Comment comment)
        {
            Id = comment.Id;
            Body = comment.Body;
            PostId = comment.PostId;
            Author = comment.Author.UserName;
            CreateTime = comment.CreateTime.ToString("g");
            Pinned = comment.Pinned;
        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public string CreateTime { get; set; }
        public bool Pinned { get; set; }
    }
}
