using System;
using System.Threading.Tasks;
using Planner.Models;

namespace Planner.Services
{
    public interface ICurrentUser
    {
        // The function to get numeric user id of the currently logged in user
        public Task<int> GetCurrentUserId();

        // The function to get user object of the currently logged in user
        public Task<User> GetCurrentUserObject();
    }
}
