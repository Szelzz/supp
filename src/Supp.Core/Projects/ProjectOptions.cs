namespace Supp.Core.Projects
{
    public class ProjectOptions
    {
        public int Id { get; set; }
        public bool FlexibleTags { get; set; }

        public ProjectOptions GetDefault()
        {
            return new ProjectOptions()
            {
                FlexibleTags = true
            };
        }
    }
}
