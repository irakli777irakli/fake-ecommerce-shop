using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {   
            // fish out from calims principal
            // Claims from all identities of principal
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        }
    }
}