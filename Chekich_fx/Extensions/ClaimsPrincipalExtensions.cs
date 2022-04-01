using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chekich_fx.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if(principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if(typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }
    }
}
