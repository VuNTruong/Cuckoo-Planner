using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Planner.Data;
using Microsoft.EntityFrameworkCore;
using Planner.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planner.Controllers
{
    [Route("user")]
    [Authorize]
    public class UserController : Controller
    {
        // Auto mapper
        private IMapper _mapper;

        // Constructor
        public UserController (IMapper mapper)
        {
            _mapper = mapper;
        }

        // The view where user can see account info
        [HttpGet("accountInfo")]
        public async Task<IActionResult> AccountInfo()
        {
            ViewData["Header"] = "Account info";

            // Get user id of the currently logged in user
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // The database context
            var databaseContext = new DatabaseContext();

            // Reference the database, include user identity object as well
            var currentUserObject = await databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.User.Id == currentUserId);

            // Map user profile object into account info view model
            var accountInfoViewModel = _mapper.Map<AccountInfoViewModel>(currentUserObject);

            return View(accountInfoViewModel);
        }
    }
}
