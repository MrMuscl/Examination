using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Areas.Identity.Data
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager) 
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                var adminUser = new IdentityUser();
                adminUser.UserName = "Admin";
                adminUser.Email = "admin@examination.ru";

                IdentityResult res = userManager.CreateAsync(adminUser, "Admin$").Result;
                if (res.Succeeded) 
                {
                    userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
                }
            }

            if (userManager.FindByNameAsync("Ivan").Result == null) 
            {
                var ivanUser = new IdentityUser();
                ivanUser.UserName = "Ivan";
                ivanUser.Email = "ivan@examination.ru";

                var res = userManager.CreateAsync(ivanUser, "Ivan$").Result;
                if (res.Succeeded) 
                {
                    userManager.AddToRoleAsync(ivanUser, "Student").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager) 
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result) 
            {
                var role = new IdentityRole { Name = "Administrator" };
                IdentityResult res = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                var role = new IdentityRole { Name = "Student" };
                IdentityResult res = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
