namespace Supp.Core.Projects
{
    public class ProjectRole
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public ProjectRoleType Type { get; set; }
    }
}