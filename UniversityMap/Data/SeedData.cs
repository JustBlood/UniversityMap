using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityMap.Models;
using UniversityMap.ViewModels;

namespace UniversityMap.Data
{
    public class SeedData
    {
        public async static Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var email = "msg.msgq@mail.ru";
            string password = "1q2w#E$R";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(email) == null)
            {
                var admin = new User()
                {
                    UserName = email,
                    Email = email
                };
                
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
