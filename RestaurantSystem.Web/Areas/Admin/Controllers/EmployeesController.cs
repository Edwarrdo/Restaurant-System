using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Admin.Models.BindingModels;
using RestaurantSystem.Web.Areas.Admin.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class EmployeesController : AdminController
    {
        private readonly RMSContext context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public EmployeesController(RMSContext context, IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllEmployees()
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreationBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var employee = this.mapper.Map<User>(model);
            employee.UserName = employee.FirstName + employee.LastName;
            await this.userManager.CreateAsync(employee, model.Password);
            await userManager.AddToRoleAsync(employee, model.Role);
            this.TempData["message"] = $"Employee {employee.FirstName} successfully hired!";
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var employee = this.context.Users.FirstOrDefault(e => e.Id == id);
            if(employee == null)
            {
                return NotFound();
            }

            var model = this.mapper.Map<EmployeeDetailsViewModel>(employee);

            await FindUserProfession(employee, model);
            return this.View(model);
        }

        private async Task FindUserProfession(User currentUser, EmployeeDetailsViewModel model)
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

        //TODO Implement Fire method

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