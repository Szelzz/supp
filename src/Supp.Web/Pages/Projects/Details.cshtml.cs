﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supp.Core.Authorization;
using Supp.Core.Modifier;
using Supp.Core.Projects;
using Supp.Core.Tags;

namespace Supp.Web.Pages.Projects
{
    [IgnoreAntiforgeryToken(Order = 2000)] // Temporary
    public class DetailsModel : PageModel
    {
        private readonly ProjectService projectService;
        private readonly UniversalModelModifier modelModifier;
        private readonly TagService tagService;
        private readonly PermissionService permissionService;
        private readonly ProjectPermissionService projectPermissionService;

        public DetailsModel(ProjectService projectService,
            UniversalModelModifier modelModifier,
            TagService tagService,
            PermissionService permissionService,
            ProjectPermissionService projectPermissionService)
        {
            this.projectService = projectService;
            this.modelModifier = modelModifier;
            this.tagService = tagService;
            this.permissionService = permissionService;
            this.projectPermissionService = projectPermissionService;
        }

        public Project Project { get; set; }
        public string ProjectTags { get; set; }
        public List<UserRoleFlattened> UserRoles { get; set; }

        public class UserRoleFlattened
        {
            public UserRoleFlattened(string username, Role role)
            {
                Username = username;
                Role = role;
            }

            public string Username { get; set; }
            public Role Role { get; set; }
        }

        public async Task<IActionResult> OnGet(int projectId)
        {
            Project = await projectService.GetAsync(projectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            ProjectTags = string.Join(",", (await tagService.GetForProjectAsync(projectId)).Select(t => t.Name));

            var userRoles = await projectPermissionService.GetRolesForProjectAsync(projectId);
            UserRoles = userRoles.Select(r => new UserRoleFlattened(r.User.UserName, r.Role)).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostEdit([FromBody] EditData model)
        {
            Project = await projectService.GetAsync(model.ModelId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            var result = modelModifier.SetValue(Project, model.PropertyName, model.Value);
            if (!result.Succeeded)
                return BadRequest();

            await projectService.EditAsync(Project);
            return new JsonResult(model.Value);
        }

        public async Task<IActionResult> OnPostTagAdd([FromBody] TagInfo tagInfo)
        {
            Project = await projectService.GetAsync(tagInfo.ProjectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            var resultTag = await tagService.AddToProjectAsync(tagInfo.ProjectId, tagInfo.TagName);
            if (resultTag == null)
                return new JsonResult("Invalid name")
                {
                    StatusCode = 400
                };

            return new JsonResult(resultTag);
        }

        public async Task<IActionResult> OnPostTagRemove([FromBody] TagInfo tagInfo)
        {
            Project = await projectService.GetAsync(tagInfo.ProjectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();


            await tagService.RemoveTagAsync(tagInfo.ProjectId, tagInfo.TagName);

            return new JsonResult("Ok");
        }

        public async Task<IActionResult> OnPostAddRole([FromBody] NewRoleInfo roleInfo)
        {
            Project = await projectService.GetAsync(roleInfo.ProjectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            var success = await projectPermissionService.AddRoleAsync(roleInfo.ProjectId, roleInfo.Role, roleInfo.Username);
            if (success)
                return new JsonResult("Ok");
            else
                return new JsonResult("Nierawidłowe dane")
                {
                    StatusCode = 500
                };
        }

        public async Task<IActionResult> OnPostRemoveRole([FromBody] NewRoleInfo roleInfo)
        {
            Project = await projectService.GetAsync(roleInfo.ProjectId);
            if (Project == null)
                return NotFound();

            if (!permissionService.Authorize(Permission.ProjectCanModify, Project))
                return new ForbidResult();

            await projectPermissionService.RemoveRoleAsync(roleInfo.ProjectId, roleInfo.Role, roleInfo.Username);
            return new JsonResult("Ok");
        }

        public class NewRoleInfo
        {
            public int ProjectId { get; set; }
            public string Username { get; set; }
            public Role Role { get; set; }
        }

        public class TagInfo
        {
            public int ProjectId { get; set; }
            public string TagName { get; set; }
        }
    }
}
