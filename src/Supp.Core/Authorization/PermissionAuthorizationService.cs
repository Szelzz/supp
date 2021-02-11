﻿using Supp.Core.Projects;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Authorization
{
    public class PermissionAuthorizationService
    {
        private static readonly Dictionary<Role, List<Permission>> rolePermissionsMap = new Dictionary<Role, List<Permission>>();

        private readonly ClaimsPrincipal user;

        static PermissionAuthorizationService()
        {
            LoadRolePermissions();
        }

        public PermissionAuthorizationService(ClaimsPrincipal user)
        {
            this.user = user;
        }

        private static void LoadRolePermissions()
        {
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                var permissions = typeof(Role).GetMember(role.ToString())[0]
                    .GetCustomAttributes(typeof(PermissionRoleAttribute), false)
                    .Cast<PermissionRoleAttribute>()
                    .Select(a => a.Permission).ToList();
                rolePermissionsMap.Add(role, permissions);
            }
        }

        public bool Authorize(Permission permission, IResource resource = null)
        {
            foreach (var claim in user.Claims.Where(c=>c.Type == PermissionClaim.ClaimType))
            {
                var permissionClaim = new PermissionClaim(claim);
                if (!rolePermissionsMap[permissionClaim.Role].Any(p => p == permission))
                    continue;

                if (permissionClaim.ResourceId == resource?.Id)
                    return true;
            }
            return false;
        }
    }
}