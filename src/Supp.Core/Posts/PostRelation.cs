namespace Supp.Core.Posts
{
    public class PostRelation
    {

        public PostRelation() { }

        public PostRelation(int parentId, int childId)
        {
            ParentId = parentId;
            ChildId = childId;
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }

        public Post Parent { get; set; }
        public Post Child { get; set; }
    }
}