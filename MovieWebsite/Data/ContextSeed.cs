using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Enums;

namespace MovieWebsite.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                //Seed Roles
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Moderator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
            }
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "huetruong100802@gmail.com",
                Email = "huetruong100802@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Email != defaultUser.Email))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123456");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }
            }
        }
        public static async Task SeedMovieRole(IServiceProvider serviceProvider)
        {
            using var context = new MovieDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MovieDbContext>>()
                    );
            if (context.Roles.Any())
            {
                return;
            }
            context.Roles.AddRange(
                new Role
                {
                    Name = MovieRoles.Actor.ToString(),
                    Id= MovieRoles.Actor.ToString(),
                },
                new Role
                {
                    Name = MovieRoles.Director.ToString(),
                    Id= MovieRoles.Director.ToString(),
                }
                ) ;
            await context.SaveChangesAsync();
        }
        public static async Task SeedMovieStatus(IServiceProvider serviceProvider)
        {
            using var context = new MovieDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MovieDbContext>>()
                    );
            if (context.Statuses.Any())
            {
                return;
            }
            context.Statuses.AddRange(
                new Status
                {
                    Name="HD Vietsub"
                },
                new Status
                {
                    Name = "Thuyết minh"
                },
                new Status
                {
                    Name = "HD Engsub"
                }
                );
            await context.SaveChangesAsync();
        }
    }
}
