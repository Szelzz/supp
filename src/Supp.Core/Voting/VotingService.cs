using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supp.Core.Data.EF;
using Supp.Core.Posts;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Voting
{
    public class VotingService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ClaimsPrincipal currentUser;
        private readonly UserManager<User> userManager;

        public VotingService(ApplicationDbContext dbContext, ClaimsPrincipal currentUser, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.currentUser = currentUser;
            this.userManager = userManager;
        }

        public async Task VoteUpAsync(Post post)
        {
            var userId = userManager.GetUserId(currentUser);
            var vote = await dbContext.Votes.FirstOrDefaultAsync(v => v.PostId == post.Id && v.UserId == userId);
            if (vote != null)
                return;

            vote = new Vote()
            {
                PostId = post.Id,
                UserId = userId
            };
            dbContext.Add(vote);
            await dbContext.SaveChangesAsync();
        }

        public async Task UndoAsync(Post post)
        {
            var userId = userManager.GetUserId(currentUser);
            var vote = await dbContext.Votes.FirstOrDefaultAsync(v => v.PostId == post.Id && v.UserId == userId);
            if (vote == null)
                return;

            dbContext.Remove(vote);
            await dbContext.SaveChangesAsync();
        }

        public Task<int> CountVotesAsync(Post post)
        {
            return dbContext.Votes.CountAsync(v => v.PostId == post.Id);
        }

        public Task<bool> UserVoted(Post post)
        {
            var userId = userManager.GetUserId(currentUser);
            return dbContext.Votes.AnyAsync(v => v.PostId == post.Id && v.UserId == userId);
        }
    }
}
