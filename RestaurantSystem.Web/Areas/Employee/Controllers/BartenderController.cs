﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Constants;
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
                this.TempData[WebConstants.BadMessage] = "Could not take drinks!";
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = "Drinks taken!";
            }

            return RedirectToAction("DrinksWithoutBartender", "Bartender", new { area = "Employee" });
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
                this.TempData[WebConstants.BadMessage] = "Could not finish drink order!";
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = "Drinks finished!";
            }

            return RedirectToAction("TakenDrinksOrders", "Bartender", new { area = "Employee" });
        }
    }
}