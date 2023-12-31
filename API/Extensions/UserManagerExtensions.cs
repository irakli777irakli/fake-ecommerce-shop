using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {   
        // eager loading Address from User entity
        public static async Task<AppUser> FindByEmailWithAddressAsync(
            this UserManager<AppUser> input, ClaimsPrincipal user)
        {   
            // fish out from calims principal
            // Claims from all identities of principal
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);


        }
        
        // getting user, with email which got from token
        public static async Task<AppUser> FindByEmailFromClaimsPrincipal(
            this UserManager<AppUser> input,
             ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);

        }
    }
}