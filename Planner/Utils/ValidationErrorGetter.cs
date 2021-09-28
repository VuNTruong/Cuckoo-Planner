using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Planner.Utils
{
    public class ValidationErrorGetter
    {
        // Model state entry
        public ModelStateDictionary ModelState { get; set; }

        // The function to generate list of validation errors for the model
        public List<string> ValidationErrorsGenerator ()
        {
            // List of errors
            List<string> listOfErrors = new List<string>();

            // Get errors
            foreach (ModelStateEntry modelState in ModelState.Values)
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
