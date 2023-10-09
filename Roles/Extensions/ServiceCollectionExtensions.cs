using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Roles.Data;

namespace Roles.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));

            options.AddPolicy("ManagerAccess", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin")
                    || context.User.IsInRole("Manager")));

            options.AddPolicy("UserAccess", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin")
                    || context.User.IsInRole("Manager")
                    || context.User.IsInRole("User")));
        });
        return services;
    }
}