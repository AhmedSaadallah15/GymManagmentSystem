using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed;

public static class IdentityDbContextSeeding
{
    public static  bool SeedData(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
    {

		try
		{
            var hasRols = roleManager.Roles.Any();
            var hasUsers = userManager.Users.Any();
            if (hasRols && hasUsers) return false;

            if (!hasRols)
            {
                var Roles = new List<IdentityRole>()
            {
                new (){Name = "SuperAdmin"},
                new (){Name = "Admin"}
            };

                foreach (var Role in Roles)
                {
                    if (!roleManager.RoleExistsAsync(Role.Name!).Result)
                    {
                        roleManager.CreateAsync(Role).Wait();
                    }
                }
            }

            if (!hasUsers)
            {
                var mainAdmin = new ApplicationUser()
                {
                    FristName = "Ahmed",
                    LastName = "Saadallah",
                    UserName = "AhmedSaadallah",
                    Email = "AhmedSaadallah@gmail.com",
                    PhoneNumber = "01212518190"
                };

                userManager.CreateAsync(mainAdmin , "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(mainAdmin , "SuperAdmin").Wait();


                var Admin = new ApplicationUser()
                {
                    FristName = "Omar",
                    LastName = "Zian",
                    UserName = "OmarZian",
                    Email = "OmarZian@gmail.com",
                    PhoneNumber = "01212518199"
                };

                userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(Admin, "Admin").Wait();
            }

            return true;
        }
		catch (Exception ex)
		{

            Console.WriteLine($"Faild Seed Data {ex}");
            return false;
		}

    }





}
