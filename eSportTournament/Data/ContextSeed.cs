using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.Data
{
    public enum Roles
    {
      Licencie,
      Admin
    }
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Licencie.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
          
        }
    }
}
