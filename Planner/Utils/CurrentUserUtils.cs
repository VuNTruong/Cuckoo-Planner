using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Planner.Data;
using Planner.Models;

namespace Planner.Utils
{
    public class CurrentUserUtils
    {
        // User id of the currently logged in user
        private readonly string currentUserId;

        // Database context
        private readonly DatabaseContext databaseContext;

        // Constructor
        public CurrentUserUtils(IHttpContextAccessor httpContextAccessor)
        {
            // Http context accessor is injected in here via DI
            // Get user id of the currently logged in user
            currentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Initialize database context
            databaseContext = new DatabaseContext();
        }

        // The function to get user id of the currently logged in user (numeric)
        public async Task<int> GetCurrentUserId()
        {
            // Reference the database, include user identity object as well
            var currentUserObject = (await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Where(userProfile => userProfile.User.Id == currentUserId)
                .ToListAsync())[0];

            // Return the obtained user id
            return currentUserObject.Id;
        }

        // The function to get user object of the currently logged in user
        public async Task<User> GetCurrentUserObject()
        {
            // Reference the database, include user identity object as well
            User currentUserObject = (await databaseContext.Users
                .Where(user => user.Id == currentUserId).ToListAsync())[0];

            // Return the obtained user object
            return currentUserObject;
        }
    }
}
