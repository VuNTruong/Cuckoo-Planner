using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Planner.Services
{
    public interface IErrorGetter
    {
        // The function to get login error from Identity
        public Task<List<string>> GetLoginError(string userEmail, SignInResult signInResult);

        // The function to get validation error
        public List<string> ValidationErrorsGenerator(ModelStateDictionary modelStateDictionary);
    }
}
