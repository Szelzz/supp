using Supp.Core.Projects;
using Supp.Core.Users;
using System;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Supp.Core.Authorization
{
    public class PermissionClaim : Claim
    {
        public const string ClaimType = "PermissionClaim";

        public PermissionClaim(UserRole role)
            : base(ClaimType, GetValue(role))
        {
            Role = role.Role;
            ProjectId = role.ProjectId;
        }

        public PermissionClaim(Claim claim)
            : base(claim)
        {
            if (claim.Type != ClaimType)
                throw new InvalidOperationException("Invalid claim type");

            var match = Regex.Match(claim.Value, "Role:(\\w+);ResourceId:([0-9]*)");
            if (!match.Success)
                throw new InvalidOperationException("Invalid claim value");

            Role = (Role)Enum.Parse(typeof(Role), match.Groups[1].Value);
            if (match.Groups[1].Value != null)
                ProjectId = int.Parse(match.Groups[2].Value);
        }


        public Role Role { get; }
        public int? ProjectId { get; }

        private static string GetValue(UserRole role)
            => $"Role:{role.Role};ResourceId:{role.ProjectId}";
    }
}
