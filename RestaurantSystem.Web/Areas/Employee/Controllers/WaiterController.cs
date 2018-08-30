using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Employee.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class WaiterController : EmployeeController
    {
        private RMSContext context;
        private IMapper mapper;

        public WaiterController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult OrdersForApproval()
        {
            var orders = this.context.Orders.Where(o => o.Approved == false);
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var user = this.GetCurrentUser();
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            order.Approved = true;
            order.WaiterId = user.Id;
            await this.context.SaveChangesAsync();

            return RedirectToAction("OrdersForApproval", "Waiter", new { area = "Employee" });
        }

        [HttpGet]
        public IActionResult FinishedOrders()
        {
            var orders = this.context.Orders.Where(o => o.IsFinished && o.TimeOfPayment == null);
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> PayForOrder(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            if (order.IsBeingCooked || order.DrinksAreBeingPrepped || (order.OrderDrinks != null && !order.DrinksAreFinished) || (order.OrderFoods != null && !order.MealsAreFinished))
            {
                TempData["notFinishedMessage"] = "Some things have not been delivered yet!";
                return RedirectToAction("FinishedOrders", "Waiter", new { area = "Employee" });
            }
            order.TimeOfPayment = DateTime.Now;
            await this.context.SaveChangesAsync();
            TempData["finishedMessage"] = "Order finished!";
            return RedirectToAction("FinishedOrders", "Waiter", new { area = "Employee" });
        }

        private User GetCurrentUser()
        {
            var name = this.User.Identity.Name;
            var user = this.context.Users.FirstOrDefault(u => u.UserName == name);
            return user;
        }
    }
}