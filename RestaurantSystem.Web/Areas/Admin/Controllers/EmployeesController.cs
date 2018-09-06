using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Constants;
using RestaurantSystem.Services.Admin.Interfaces;
using RestaurantSystem.Services.Exceptions;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreationBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var result = await this.employeesService.CreateAsync(model);
            if (result == 0)
            {
                this.TempData[WebConstants.GoodMessage] = $"Employee {model.FirstName} couldn't be hired!";
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = $"Employee {model.FirstName} successfully hired!";
            }
                return RedirectToAction("AllEmployees", "Employees", new { area = WebConstants.AdminArea });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var model = await employeesService.GetEmployeeDetailsModelAsync(id);
                return this.View(model);
            }
            catch(NotFoundException e)
            {
                this.TempData[WebConstants.BadMessage] = "There is no employee with such id!";
                return this.RedirectToAction("AllEmployees", "Employees", new { Area = WebConstants.AdminArea });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Fire(string id)
        {
            var result = await this.employeesService.FireEmployeeAsync(id);
            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = $"Something went wrong!";
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = $"Employee successfully fired!";
            }
            return RedirectToAction("AllEmployees", "Employees", new { area = WebConstants.AdminArea });
        }
    }
}