using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Roles.Data;

namespace Roles.Extensions;

public class SeedData
{
    public static Task Seed(ApplicationDbContext dbContext,IServiceProvider serviceProvider)
    {
        try
        {
            var userMgr = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            dbContext.Database.Migrate();

            var adminRole = new IdentityRole("Admin");

            if (!dbContext.Roles.Any())
            {
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (!dbContext.Users.Any(u => u.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };
                var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    if (adminRole.Name != null) userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return Task.CompletedTask;
    }
}