using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Admin.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private RMSContext context;
        private IMapper mapper;
        private UserManager<User> userManager;

        public HomeController(RMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = this.context.Users
                .Where(u => u.IsEmployee == true)
                .ToArray();
            var userModels = this.mapper.Map<List<EmployeeConciseViewModel>>(users);
            for (int i = 0; i < users.Count(); i++)
            {
                await FindUserProfession(users[i], userModels[i]);
            }
            return View(userModels);
        }

        private async Task FindUserProfession(User currentUser, EmployeeConciseViewModel model)
        {
            if (await userManager.IsInRoleAsync(currentUser, "Chef"))
            {
                model.Profession = "Chef";
            }
            else if (await userManager.IsInRoleAsync(currentUser, "Bartender"))
            {
                model.Profession = "Bartender";
            }
            else
            {
                model.Profession = "Waiter";
            }
        }
    }
}