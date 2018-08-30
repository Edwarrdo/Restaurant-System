using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Web.Areas.Employee.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class BartenderController : EmployeeController
    {
        private RMSContext context;
        private IMapper mapper;

        public BartenderController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult DrinksWithoutBartender()
        {
            var orders = this.context.Orders.Where(o => o.Approved && !o.DrinksAreFinished && !o.DrinksAreBeingPrepped && o.OrderDrinks != null && o.OrderDrinks.Any());
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> BartenderTakeOrder(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            order.DrinksAreBeingPrepped = true;
            await this.context.SaveChangesAsync();

            return RedirectToAction("DrinksWithoutBartender", "Bartender", new { area = "Employee" });
        }

        [HttpGet]
        public IActionResult TakenDrinksOrders()
        {
            var orders = this.context.Orders.Where(o => o.DrinksAreBeingPrepped);
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> FinishDrinksOrder(int id)
        {
            var order = this.context.Orders.Include(o => o.OrderFoods).FirstOrDefault(o => o.Id == id);
            if (order.MealsAreFinished || order.OrderFoods == null)
            {
                order.IsFinished = true;
            }
            order.DrinksAreBeingPrepped = false;
            order.DrinksAreFinished = true;
            await this.context.SaveChangesAsync();

            return RedirectToAction("TakenDrinksOrders", "Bartender", new { area = "Employee" });
        }
    }
}