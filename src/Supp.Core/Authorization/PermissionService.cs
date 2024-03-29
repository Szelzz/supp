﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using Supp.Core.Projects;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Supp.Core.Authorization
{
    public class PermissionService
    {
        private static readonly Dictionary<Role, List<Permission>> rolePermissionsMap = new Dictionary<Role, List<Permission>>();

        private readonly ClaimsPrincipal user;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly Permission[] allowedForAuthor = new[]
        {
            Permission.PostCanModify,
            Permission.CommentCanRemove,
        };

        static PermissionService()
        {
            LoadRolePermissions();
        }

        public PermissionService(ClaimsPrincipal user, ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.user = user;
            this.dbContext = dbContext;
            this.userManager = userManager;
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

        public bool Authorize(Permission permission, Project resource = null)
        {
            foreach (var claim in user.Claims.Where(c => c.Type == PermissionClaim.ClaimType))
            {
                var permissionClaim = new PermissionClaim(claim);
                if (!rolePermissionsMap[permissionClaim.Role].Any(p => p == permission))
                    continue;

                if (permissionClaim.ProjectId == resource?.Id)
                    return true;
            }
            return false;
        }

        public bool Authorize(Permission permission, IProjectResource projectResource)
        {
            // allow all permissions if current user is author of post
            if (projectResource.GetAuthorId() == userManager.GetUserId(user) && allowedForAuthor.Contains(permission))
            {
                return Authorize(Permission.ProjectCanRead, projectResource.GetProject());
            }

            return Authorize(permission, projectResource.GetProject());
        }

        public IEnumerable<Project> AuthorizeCollection(Permission permission, IEnumerable<Project> resources)
        {
            var collection = new List<Project>();
            foreach (var claim in user.Claims.Where(c => c.Type == PermissionClaim.ClaimType))
            {
                var permissionClaim = new PermissionClaim(claim);
                if (!rolePermissionsMap[permissionClaim.Role].Any(p => p == permission))
                    continue;
                foreach (var resource in resources)
                {
                    if (permissionClaim.ProjectId == resource?.Id)
                        collection.Add(resource);
                }
            }
            return collection.Distinct();
        }

        public async Task GrantRoleAsync(Role role, Project resource = null)
        {
            if (user.Claims
                .Where(c => c.Type == PermissionClaim.ClaimType)
                .Select(c => new PermissionClaim(c))
                .Any(c => c.Role == role && c.ProjectId == resource?.Id))
                return; // already granted

            var userRole = new UserRole()
            {
                Role = role,
                UserId = userManager.GetUserId(user),
                ProjectId = resource?.Id
            };
            dbContext.Add(userRole);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> GrantRoleForUserAsync(User user, Role role, Project resource = null)
        {
            var claims = await userManager.GetClaimsAsync(user);
            if (claims
                .Where(c => c.Type == PermissionClaim.ClaimType)
                .Select(c => new PermissionClaim(c))
                .Any(c => c.Role == role && c.ProjectId == resource?.Id))
                return false; // already granted

            var userRole = new UserRole()
            {
                Role = role,
                UserId = user.Id,
                ProjectId = resource?.Id
            };
            dbContext.Add(userRole);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task RemoveRoleFromUserAsync(User user, Role role, Project resource = null)
        {
            var userRole = await dbContext.UserRoles.FirstOrDefaultAsync(r => r.UserId == user.Id && r.Role == role && r.ProjectId == resource.Id);
            if (userRole == null)
                return;

            dbContext.Remove(userRole);
            await dbContext.SaveChangesAsync();
        }
    }
}
