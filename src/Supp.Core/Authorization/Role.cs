using Supp.Core.Projects;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Supp.Core.Authorization
{
    public enum Role
    {
        Admin = 1,
        User = 2,

        // resource based

        [Display(Name = "Właściciel projektu")]
        [ResourceRole(typeof(Project))]
        [PermissionRole(Permission.ProjectCanRead)]
        [PermissionRole(Permission.ProjectCanModify)]
        ProjectOwner = 10,

        [Display(Name = "Właściciel projektu")]
        [PermissionRole(Permission.ProjectCanRead)]
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
        ProjectCanModify
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
