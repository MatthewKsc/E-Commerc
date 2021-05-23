using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityDbContextSeeder
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager) {
            if (!userManager.Users.Any()) {
                var user = new AppUser {
                    DisplayName = "System",
                    Email = "system@test.com",
                    UserName = "system@test.com",
                    Address = new Address {
                        FirstName = "System",
                        LastName = "Sys",
                        Street = "Comapny Street",
                        City = "Katowice",
                        State = "Slaske",
                        ZipCode = "40-001",

                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
