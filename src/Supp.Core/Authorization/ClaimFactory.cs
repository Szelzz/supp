using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Supp.Core.Data.EF;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Authorization
{
    public class ClaimFactory : UserClaimsPrincipalFactory<User>

    {
        private readonly ApplicationDbContext dbContext;

        public ClaimFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor, ApplicationDbContext dbContext)
            : base(userManager, optionsAccessor)
        {
            this.dbContext = dbContext;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;
            var userRoles = dbContext.UserRoles.Where(r => r.UserId == user.Id).ToList();
            foreach (var userRole in userRoles)
                identity.AddClaim(new PermissionClaim(userRole));

            return principal;
        }
    }
}
