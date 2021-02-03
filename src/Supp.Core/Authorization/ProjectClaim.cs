using Supp.Core.Projects;
using System;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Supp.Core.Authorization
{
    public class ProjectClaim : Claim
    {
        private const string claimType = "ProjectClaim";


        public ProjectClaim(int projectId, ProjectRoleType role)
            : base(claimType, GetValue(projectId, role))
        {
            this.Role = role;
            this.ProjectId = projectId;
        }

        public ProjectClaim(Claim claim)
            : base(claim)
        {
            if (claim.Type != claimType)
                throw new InvalidOperationException("Invalid claim type");

            var match = Regex.Match(claim.Value, "Project:([0-9]+);Role:(\\w+)");
            if (!match.Success)
                throw new InvalidOperationException("Invalid claim value");

            ProjectId = int.Parse(match.Groups[1].Value);
            Role = (ProjectRoleType)Enum.Parse(typeof(ProjectRoleType), match.Groups[2].Value);
        }

        public ProjectRoleType Role { get; }
        public int ProjectId { get; }

        private static string GetValue(int projectId, ProjectRoleType role)
        {
            return $"Project:{projectId};Role:{role}";
        }

        public static Predicate<Claim> IsClaim(int projectId, ProjectRoleType role)
        {
            var value = GetValue(projectId, role);
            return c => c.Type == claimType && c.Value == value;
        }
    }
}
