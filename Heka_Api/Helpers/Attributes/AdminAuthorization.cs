using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Heka.Api.Helpers.Attributes
{
    public class AdminAuthorization: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                throw new AuthenticationException("You have to logged in first");

            if (context.HttpContext.User.FindFirstValue("Role").ToString() != "admin")
            {
                throw new AuthenticationException("Only admin can have access to this url");
            }
        }
    }
}
