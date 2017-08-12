using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;

namespace PresentationLayer.Services
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class PermissionRole : AuthorizeAttribute
    {
        public string[] AllowedPermissions { get; private set; }

        public PermissionRole(string allowedPermissions)
        {
            string[] arrayRole = allowedPermissions.Split(',');
            AllowedPermissions = arrayRole;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            foreach (var perm in AllowedPermissions)
            {
                if (HasPermission(perm,actionContext))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            
        }

        private bool HasPermission(string permission, HttpActionContext context)
        {
            var result= context.RequestContext.Principal.IsInRole(permission);
            
            return result;
        }
    }
}