using Supp.Core.Projects;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Supp.Core.Authorization
{
    public enum Role
    {
        [Display(Name = "Właściciel projektu")]
        [ResourceRole(typeof(Project))]
        [PermissionRole(Permission.ProjectCanRead)]
        [PermissionRole(Permission.ProjectCanModify)]
        [PermissionRole(Permission.PostCanRead)]
        [PermissionRole(Permission.PostCanModify)]
        [PermissionRole(Permission.PostCanVote)]
        [PermissionRole(Permission.CommentCanAdd)]
        [PermissionRole(Permission.CommentCanPin)]
        ProjectOwner = 10,

        [Display(Name = "Członek projektu")]
        [PermissionRole(Permission.ProjectCanRead)]
        [PermissionRole(Permission.PostCanRead)]
        [PermissionRole(Permission.PostCanVote)]
        [PermissionRole(Permission.CommentCanAdd)]
        [ResourceRole(typeof(Project))]
        ProjectVisitor = 11
    }

    public static class RoleHelper
    {
        public static Role[] ProjectRoles => new[] {
            Role.ProjectOwner,
            Role.ProjectVisitor
        };
    }

    public enum Permission
    {
        // Project resource
        ProjectCanRead,
        ProjectCanModify,

        PostCanRead,
        PostCanModify,
        PostCanVote,

        CommentCanAdd,
        CommentCanRemove,
        CommentCanPin
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class ResourceRoleAttribute : Attribute
    {
        public ResourceRoleAttribute(Type resourceType)
        {
            ResourceType = resourceType;
        }

        public Type ResourceType { get; }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    sealed class PermissionRoleAttribute : Attribute
    {
        public PermissionRoleAttribute(Permission permission)
        {
            this.Permission = permission;
        }

        public Permission Permission { get; }
    }
}
