using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Planner.Services
{
    public interface IHttpUtils
    {
        JsonResult GetResponseData(int statusCode, List<string> listOfKeys, List<object> listOfData, HttpResponse response);
    }
}
