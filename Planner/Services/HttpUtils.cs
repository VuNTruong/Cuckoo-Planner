using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Planner.Services
{
    public class HttpUtils : IHttpUtils
    {
        public JsonResult GetResponseData(int statusCode, List<string> listOfKeys, List<object> listOfData, HttpResponse response)
        {
            // Prepare the response data
            var responseData = new Dictionary<string, object>();

            for (int i = 0; i < listOfKeys.Count; i++)
            {
                responseData.Add(listOfKeys[i], listOfData[i]);
            }

            // Set the status code
            response.StatusCode = statusCode;

            // Return response to the client
            return new JsonResult(responseData);
        }
    }
}
