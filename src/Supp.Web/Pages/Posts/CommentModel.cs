using Supp.Core.Posts;

namespace Supp.Web.Pages.Posts
{
    public class CommentModel
    {
        public CommentModel() { }
        public CommentModel(Comment comment)
        {
            Body = comment.Body;
            PostId = comment.PostId;
            Author = comment.Author.UserName;
            CreateTime = comment.CreateTime.ToString("g");
        }

        public int PostId { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public string CreateTime { get; set; }
    }
}
