using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Constants;
using RestaurantSystem.Common.Employee.ViewModels;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class HomeController : EmployeeController
    {
        private readonly RMSContext context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public HomeController(RMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var currentUser = context.Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var model = mapper.Map<EmployeeDetailsViewModel>(currentUser);
            await FindUserProfession(currentUser, model);
            return View(model);
        }

        private async Task FindUserProfession(User currentUser, EmployeeDetailsViewModel model)
        {
            if (await userManager.IsInRoleAsync(currentUser, WebConstants.ChefRole))
            {
                model.Profession = WebConstants.ChefRole;
            }
            else if (await userManager.IsInRoleAsync(currentUser, WebConstants.BartenderRole))
            {
                model.Profession = WebConstants.BartenderRole;
            }
            else
            {
                model.Profession = WebConstants.WaiterRole;
            }
        }
    }
}