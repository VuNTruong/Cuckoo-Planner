using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Planner.Models;

namespace Planner.Services
{
    public class ErrorGetter : IErrorGetter
    {
        // User manager
        private UserManager<User> _userManager;

        // Constructor
        public ErrorGetter (UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // Implement the GetLoginError function
        public async Task<List<string>> GetLoginError(string userEmail, SignInResult signInResult)
        {
            // List of errors
            List<string> listOfErrors = new List<string>();

            // Call the function to find user object based on email
            var userObject = await _userManager.FindByEmailAsync(userEmail);

            // Check for sign in errors
            if (signInResult.IsNotAllowed)
            {
                if (!await _userManager.IsEmailConfirmedAsync(userObject))
                {
                    // Email is not confirmed
                    listOfErrors.Add("Email is not confirmed");
                }

                if (!await _userManager.IsPhoneNumberConfirmedAsync(userObject))
                {
                    // Phone number is not confirmed
                    listOfErrors.Add("Phone number is not confirmed");
                }
            }
            else if (signInResult.IsLockedOut)
            {
                // Account is locked out
                listOfErrors.Add("You are locked out at this point");
            }
            else if (signInResult.RequiresTwoFactor)
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

        // Implement the ValidationErrorGenerator function
        public List<string> ValidationErrorsGenerator(ModelStateDictionary modelStateDictionary)
        {
            // List of errors
            List<string> listOfErrors = new List<string>();

            // Get errors
            foreach (ModelStateEntry modelState in modelStateDictionary.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    // Add error to list of errors
                    listOfErrors.Add(error.ErrorMessage);
                }
            }

            // Return list of errors
            return listOfErrors;
        }
    }
}
