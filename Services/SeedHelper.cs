using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HamperWeb.Services;

namespace HamperWeb.Services
{
    public static class SeedHelper
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
  //          DataService<Category> 

            //Seed the database whith admin role
            if(await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //seed the database with the admin (Bill)
            if(await userManager.FindByNameAsync("Bill") == null)
            {
                IdentityUser userAdmin = new IdentityUser("Bill");
                await userManager.CreateAsync(new IdentityUser("Bill"), "admin1");
                //add Bill to Admin role
               await userManager.AddToRoleAsync(userAdmin, "Admin");
            }


            if (await roleManager.FindByNameAsync("Customer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            //seed the database with the admin (Bill)
            if (await userManager.FindByNameAsync("John") == null)
            {
                IdentityUser userCastomer = new IdentityUser("John");
                //add Bill to John role
                await userManager.CreateAsync(userCastomer, "John1");
            }

         
        }

    }
}
