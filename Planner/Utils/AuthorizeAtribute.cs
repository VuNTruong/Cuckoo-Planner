using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Planner.Services;

namespace Planner.Utils
{
    public class AuthorizeAtribute : Attribute, IAuthorizationFilter
    {
        // Http context accessor
        private IHttpContextAccessor _httpContextAccessor;

        // Http Utils
        private readonly IHttpUtils _httpUtils;

        public string _restrictedTo { get; }

        // Constructor
        public AuthorizeAtribute (IHttpContextAccessor httpContextAccessor, IHttpUtils httpUtils)
        {
            _httpContextAccessor = httpContextAccessor;

            // Initialize http utils
            _httpUtils = httpUtils;
        }

        public AuthorizeAtribute(string v)
        {
            _restrictedTo = v;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated && !context.ActionDescriptor.EndpointMetadata.Any(meta => meta.GetType().Name == "AllowAnonymousAttribute"))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "login",
                    controller = "Auth",
                    returnPath = context.HttpContext.Request.Path,
                    area = ""
                }));
            }

            if (!context.HttpContext.User.IsInRole(_restrictedTo))
            {
                //_httpContextAccessor.HttpContext.Response.StatusCode = 403;
                returnResponse();
            }
        }

        public JsonResult returnResponse()
        {
            // Response data for the client
            List<string> responseBodyKeys;
            List<object> responseBodyData;

            // Add data to the response data
            responseBodyKeys = new List<string> { "status", "data" };
            responseBodyData = new List<object> { "Not allowed", "You are not authorized for this operation" };

            return _httpUtils.GetResponseData(403, responseBodyKeys, responseBodyData, _httpContextAccessor.HttpContext.Response);
        }
    }
}
