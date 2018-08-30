using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Admin.Interfaces;
using RestaurantSystem.Web.Areas.Admin.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class EmployeesController : AdminController
    {
        private IAdminEmployeesService employeesService;

        public EmployeesController(IAdminEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IActionResult> AllEmployees()
        {
            var userModels = await this.employeesService.GetAllEmployeesModel();
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
            var result = await this.employeesService.CreateAsync(model);
            if (result == 0)
            {
                this.TempData["errorMessage"] = $"Employee {model.FirstName} couldn't be hired!";
            }
            else
            {
                this.TempData["message"] = $"Employee {model.FirstName} successfully hired!";
            }
                return RedirectToAction("AllEmployees", "Employees", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await employeesService.GetEmployeeDetailsModelAsync(id);
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Fire(string id)
        {
            var result = await this.employeesService.FireEmployeeAsync(id);
            if (result == 0)
            {
                this.TempData["errorMessage"] = $"Something went wrong!";
            }
            else
            {
                this.TempData["message"] = $"Employee successfully fired!";
            }
            return RedirectToAction("AllEmployees", "Employees", new { area = "Admin" });
        }
    }
}