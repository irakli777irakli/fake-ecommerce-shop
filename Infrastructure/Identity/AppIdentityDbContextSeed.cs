using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser 
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bot@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Beggins",
                        Street = "10 the stree",
                        City = "Miami",
                        State = "FL",
                        Zipcode = "90210"
                    }
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");

            }
        }
    }
}