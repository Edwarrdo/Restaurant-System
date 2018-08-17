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
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var employee = this.context.Users.FirstOrDefault(e => e.Id == id);
            if(employee == null)
            {
                return NotFound();
            }

            var model = this.mapper.Map<EmployeeDetailsViewModel>(employee);
            return this.View(model);
        }
    }
}