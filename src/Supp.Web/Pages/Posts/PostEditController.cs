using Microsoft.AspNetCore.Mvc;
using Supp.Core.Data.EF;
using Supp.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supp.Web.Pages.Posts
{
    public class PostEditController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        //public PostEditController(ApplicationDbContext dbContext, AppAuthorizationService authorizationService)
        //{
        //    this.dbContext = dbContext;
        //}

        //public IActionResult Edit(int modelId, string propertyName, string value)
        //{

        //    return BadRequest()
        //    return View();
        //}
    }
}
