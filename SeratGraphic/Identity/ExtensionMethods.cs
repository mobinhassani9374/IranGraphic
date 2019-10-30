using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SeratGraphic.Identity
{
    public static class ExtensionMethods
    {
        public static string GetFullName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(c => c.Type == "FullName")?.Value;
        }
    }
}
