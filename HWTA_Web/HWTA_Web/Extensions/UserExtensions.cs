using System;
using System.Security.Claims;

namespace HWTA_Web.Extensions
{
    public static class UserExtensions
    {
        public static long ParseUserId(this ClaimsPrincipal claims)
        {
            var result = long.TryParse(claims.FindFirst("user_id").Value, out long userId);

            if (!result)
            {
                throw new ArgumentException("Cannot parse iserId");
            }

          return userId;
        }
    }
}
