using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Planner.Authorization
{
    public class UserOperations
    {
        public static class ContactOperations
        {
            public static OperationAuthorizationRequirement Create =
              new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
            public static OperationAuthorizationRequirement Read =
              new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
            public static OperationAuthorizationRequirement Update =
              new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
            public static OperationAuthorizationRequirement Delete =
              new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
            public static OperationAuthorizationRequirement Approve =
              new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };
            public static OperationAuthorizationRequirement Reject =
              new OperationAuthorizationRequirement { Name = Constants.RejectOperationName };
        }
    }
}
