using System;
using Xunit;
using UserAPI.Services;
using Planner.Data;
using Planner.Services;

namespace UserAPI.UnitTests.Services
{
    public class UnitTest2
    {
        // Current user service (this will be used to get user id of the currently logged in user)
        private readonly ICurrentUser _currentUserService;

        public UnitTest2 (ICurrentUser currentUserservice)
        {
            _currentUserService = currentUserservice;
        }

        [Fact]
        public async void CheckCurrentUserId_Return2()
        {
            var userAPIService = new UserAPIService(_currentUserService);
            int result = await userAPIService.GetCurrentUserProfileIdAsync();

            Assert.Equal(result, 2);
        }
    }
}
