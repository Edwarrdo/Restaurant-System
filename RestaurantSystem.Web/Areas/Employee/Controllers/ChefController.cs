using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Employee.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class ChefController : EmployeeController
    {
        private RMSContext context;
        private IMapper mapper;

        public ChefController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult OrdersNotBeingCooked()
        {
            var orders = this.context.Orders.Where(o => o.Approved && !o.IsBeingCooked && !o.MealsAreFinished && o.OrderFoods != null);
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> ChefTakeOrder(int id)
        {
            var user = GetCurrentUser();
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            order.ChefId = user.Id;
            order.IsBeingCooked = true;
            await this.context.SaveChangesAsync();

            return RedirectToAction("OrdersNotBeingCooked", "Chef", new { area = "Employee" });
        }

        [HttpGet]
        public IActionResult TakenMealsOrders()
        {
            var user = GetCurrentUser();
            var orders = this.context.Orders.Where(o => o.Approved == true && o.IsBeingCooked == true && o.ChefId == user.Id);
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> FinishMealsOrder(int id)
        {
            var order = this.context.Orders.Include(o => o.OrderDrinks).FirstOrDefault(o => o.Id == id);
            if (order.DrinksAreFinished || order.OrderDrinks == null)
            {
                order.IsFinished = true;
            }
            order.IsBeingCooked = false;
            order.MealsAreFinished = true;
            await this.context.SaveChangesAsync();

            return RedirectToAction("TakenMealsOrders", "Chef", new { area = "Employee" });
        }

        private User GetCurrentUser()
        {
            var name = this.User.Identity.Name;
            var user = this.context.Users.FirstOrDefault(u => u.UserName == name);
            return user;
        }
    }
}