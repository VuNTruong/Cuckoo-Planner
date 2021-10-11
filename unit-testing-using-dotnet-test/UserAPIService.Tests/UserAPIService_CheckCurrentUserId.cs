using System;
using Xunit;
using UserAPI.Services;
using Planner.Data;
using Planner.Services;

namespace UserAPI.UnitTests.Services
{
    public class UnitTest2
    {
       public UnitTest2 ()
       {}

       [Fact]
       public async void CheckCurrentUserId_Return2()
       {
           var userAPIService = new UserAPIService(_currentUserService);
           int result = await userAPIService.GetCurrentUserProfileIdAsync();

           Assert.Equal(result, 2);
       }
    }
}
