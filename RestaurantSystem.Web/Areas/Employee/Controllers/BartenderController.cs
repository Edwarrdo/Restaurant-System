using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Constants;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Employee.Interfaces;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    [Authorize(Roles = WebConstants.BartenderRole)]
    public class BartenderController : EmployeeController
    {
        private IBartenderService bartenderService;

        public BartenderController(IBartenderService bartenderService)
        {
            this.bartenderService = bartenderService;
        }

        [HttpGet]
        public IActionResult DrinksWithoutBartender()
        {
            var ordersModel = this.bartenderService.GetDrinksWithoutBartender();
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> BartenderTakeOrder(int id)
        {
            var result = await this.bartenderService.TakeOrderAsync(id);
            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.TakeOrderFailureMessage, typeof(Drink).Name);
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.TakeOrderSuccessMessage, typeof(Drink).Name);
            }

            return RedirectToAction("DrinksWithoutBartender", "Bartender", new { area = WebConstants.EmployeeArea });
        }

        [HttpGet]
        public IActionResult TakenDrinksOrders()
        {
            var ordersModel = this.bartenderService.GetDrinksTakenByBartender();
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> FinishDrinksOrder(int id)
        {
            var result = await this.bartenderService.FinishOrderAsync(id);
            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.FinishOrderFailureMessage, typeof(Drink).Name.ToLower());
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.FinishOrderSuccessMessage, typeof(Drink).Name);
            }

            return RedirectToAction("TakenDrinksOrders", "Bartender", new { area = WebConstants.EmployeeArea });
        }
    }
}