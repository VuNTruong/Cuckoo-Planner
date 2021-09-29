using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Planner.Models;

namespace Planner.Utils
{
    public class IdentityErrorGetter
    {
        // User manager
        public UserManager<User> UserManager { get; set; }

        // Sign in result
        public SignInResult SignInResult { get; set; }

        // The function to get login error
        public async Task<List<string>> LoginErrorGenerator(string userEmail)
        {
            // List of errors
            List<string> listOfErrors = new List<string>();

            // Call the function to find user object based on email
            var userObject = await UserManager.FindByEmailAsync(userEmail);

            // Check for sign in errors
            if (SignInResult.IsNotAllowed)
            {
                if (!await UserManager.IsEmailConfirmedAsync(userObject))
                {
                    // Email is not confirmed
                    listOfErrors.Add("Email is not confirmed");
                }

                if (!await UserManager.IsPhoneNumberConfirmedAsync(userObject))
                {
                    // Phone number is not confirmed
                    listOfErrors.Add("Phone number is not confirmed");
                }
            }
            else if (SignInResult.IsLockedOut)
            {
                // Account is locked out
                listOfErrors.Add("You are locked out at this point");
            }
            else if (SignInResult.RequiresTwoFactor)
            {
                // 2FA required
                listOfErrors.Add("Two factors authentication is required for login");
            }
            else
            {
                if (userObject == null)
                {
                    // Email is not correct
                    listOfErrors.Add("It seems that you did not enter the right email");
                }
                else
                {
                    // Password is not correct
                    listOfErrors.Add("It seems that you did not enter the right password");
                }
            }

            // Return list of errors
            return listOfErrors;
        }
    }
}
