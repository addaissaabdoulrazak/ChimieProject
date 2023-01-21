using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace ChimieProject.Models
{
    namespace LoginDemo.CustomAttributes
    {
        public static class PermissionExtension
        {
            public static bool HavePermission(this Controller c, string claimValue)
            {
                var structure = c.HttpContext.User;
                 bool havePer = structure.HasClaim(claimValue, claimValue);
                    return havePer;
            }
            public static bool HavePermission(this IIdentity claims, string claimValue)
            {
                 var userClaims = claims as ClaimsIdentity;
                    bool havePer = userClaims.HasClaim(claimValue, claimValue);
                    return havePer;
            }
        }
    }
}
