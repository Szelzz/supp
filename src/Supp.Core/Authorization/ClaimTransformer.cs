using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using Supp.Core.Users;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Supp.Core.Authorization
{

    public class PermissionsClaimsTransformation : IClaimsTransformation
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public PermissionsClaimsTransformation(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var newPrincipal = principal.Clone();
            var identity = (ClaimsIdentity)newPrincipal.Identity;

            var userId = userManager.GetUserId(principal);
            var userRoles = await dbContext.UserRoles.Where(r => r.UserId == userId).ToListAsync();
            foreach (var userRole in userRoles)
                identity.AddClaim(new PermissionClaim(userRole));

            return newPrincipal;
        }
    }
}
