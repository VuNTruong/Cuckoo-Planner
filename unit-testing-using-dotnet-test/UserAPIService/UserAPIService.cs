using System;
using Planner.Services;
using Planner.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UserAPI.Services
{
    public class UserAPIService
    {
        // Current user service (this will be used to get user id of the currently logged in user)
        private readonly ICurrentUser _currentUserService;

        // Constructor
        public UserAPIService (ICurrentUser currentUserService) {
            // Initialize current user service
            _currentUserService = currentUserService;
        }

        public async Task<int> GetCurrentUserProfileIdAsync () {
            // Call the function to get user id of the currently logged in user
            int currentUserId = await _currentUserService.GetCurrentUserId();

            // The database context
            using var databaseContext = new DatabaseContext();

            // Reference the database, include user identity object as well
            var currentUserObject = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .Include(userProfile => userProfile.WorkItems)
                .FirstOrDefaultAsync((userProfile) => userProfile.Id == currentUserId);

            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            return currentUserObject.Id;
        }
    }
}
