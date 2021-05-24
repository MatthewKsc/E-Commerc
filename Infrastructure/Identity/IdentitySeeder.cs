using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentitySeeder
    {
       public static void SeedData(UserManager<AppUser> userManager) {
            SeedUser(userManager);
       }

        public static void SeedUser(UserManager<AppUser> userManager) {
            if (!userManager.Users.Any()) {
                var user = new AppUser {
                    DisplayName = "System",
                    Email = "system@test.com",
                    UserName = "system@test.com",
                    Address = new Address {
                        FirstName = "System",
                        LastName = "Sys",
                        Street = "10 The street",
                        City = "Katowice",
                        State = "slaskie",
                        ZipCode = "40-001"
                    }
                };

                var result = userManager.CreateAsync(user, "Pa$$w0rd");

                if (result.Result != IdentityResult.Success) {
                    throw new Exception("Seeding Data went wrong check seeder");
                }
            }
        }
    }
}
