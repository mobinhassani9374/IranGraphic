using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SeratGraphic.DomainModels.Entities;

namespace SeratGraphic.Data.Context
{
    public static class DatabaseInitializer
    {
        private static string[] roles = new string[2] { "Admin", "User" };

        public static void SeedData(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    var result = roleManager
                        .CreateAsync(new IdentityRole { Name = role }).Result;
                }
            }

            // admin user
            var adminUser = userManager.FindByNameAsync("09212651629").Result;

            if (adminUser == null)
            {
                var adminUserResult = userManager
                    .CreateAsync(new User
                    {
                        FullName = "مبین حسنی",
                        PhoneNumber = "09212651629",
                        RegisterDate = DateTime.Now,
                        UserName = "09212651629",
                        PhoneNumberConfirmed = true
                    }, "9197442364").Result;

                adminUser = userManager.FindByNameAsync("09212651629").Result;

                var r = userManager.AddToRoleAsync(adminUser, "Admin").Result;
            }
        }
    }
}
