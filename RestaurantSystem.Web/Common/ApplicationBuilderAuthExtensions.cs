using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RestaurantSystem.Common.Constants;
using RestaurantSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Common
{
    public static class ApplicationBuilderAuthExtensions
    {
        private const string DefaultAdminPassword = "admin123";
        private const string DefaultClientPassword = "client123";
        private const string DefaulWaiterPassword = "waiter123";
        private const string DefaultBartenderPassword = "bartender123";
        private const string DefaultChefPassword = "chef123";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole(WebConstants.AdminRole),
            new IdentityRole(WebConstants.WaiterRole),
            new IdentityRole(WebConstants.ChefRole),
            new IdentityRole(WebConstants.BartenderRole),
            new IdentityRole(WebConstants.ClientRole)
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var user = await userManager.FindByNameAsync("admin");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "admin",
                        FirstName = "Admin",
                        LastName = "Adminov",
                        Email = "admin@example.com"
                    };

                    await userManager.CreateAsync(user, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }

                user = await userManager.FindByNameAsync("client");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "client",
                        FirstName = "Client",
                        LastName = "Clientov",
                        Email = "client@example.com"
                    };

                    await userManager.CreateAsync(user, DefaultClientPassword);
                    await userManager.AddToRoleAsync(user, roles[4].Name);
                }

                user = await userManager.FindByNameAsync("waiter");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "waiter",
                        FirstName = "Waiter",
                        LastName = "Waiterov",
                        Email = "waiter@example.com"
                    };

                    await userManager.CreateAsync(user, DefaulWaiterPassword);
                    await userManager.AddToRoleAsync(user, roles[1].Name);
                }

                user = await userManager.FindByNameAsync("chef");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "chef",
                        FirstName = "Chef",
                        LastName = "Chefov",
                        Email = "chef@example.com"
                    };

                    await userManager.CreateAsync(user, DefaultChefPassword);
                    await userManager.AddToRoleAsync(user, roles[2].Name);
                }

                user = await userManager.FindByNameAsync("bartender");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "bartender",
                        FirstName = "Bartender",
                        LastName = "Bartenderov",
                        Email = "bartender@example.com"
                    };

                    await userManager.CreateAsync(user, DefaultChefPassword);
                    await userManager.AddToRoleAsync(user, roles[3].Name);
                }
            }
        }
    }
}
