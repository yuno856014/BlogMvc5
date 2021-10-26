using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WedBlogs.Extension
{
    public static class IdentityEntension
    {
        public static string GetAccountId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("AccountId");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetRoleId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("RoleId");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetCredits(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("VipCredits");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetAvatar(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Avatar");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimtype)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimtype);
            return (claim != null) ? claim.Value : string.Empty;
        }

    }
}
