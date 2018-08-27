using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Employee.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class OrdersController : EmployeeController
    {
        private RMSContext context;
        private IMapper mapper;

        public OrdersController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //TODO: divide methods in appropriate controllers (WaiterController...)

        [HttpGet]
        public IActionResult OrdersForApproval()
        {
            var orders = this.context.Orders.Where(o => o.Approved == false);
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            var order = this.context.Orders.Include(o => o.OrderFoods).Include(o => o.OrderDrinks).FirstOrDefault(o => o.Id == id);
            var orderModel = new OrderViewModel()
            {
                Price = order.Price,
                TableNumbers = order.TableNumbers,
                TimeOfOrder = order.TimeOfOrder
            };
            var drinks = new List<Drink>();
            var meals = new List<Food>();
            if (order.OrderDrinks != null)
            {
                foreach (var od in order.OrderDrinks)
                {
                    var drink = this.context.Drinks.FirstOrDefault(d => d.Id == od.DrinkId);
                    drinks.Add(drink);
                }
            }
            if (order.OrderFoods != null)
            {
                foreach (var of in order.OrderFoods)
                {
                    var food = this.context.Foods.FirstOrDefault(f => f.Id == of.FoodId);
                    meals.Add(food);
                }
            }

            orderModel.Drinks = this.mapper.Map<IEnumerable<DrinkConciseViewModel>>(drinks);
            orderModel.Meals = this.mapper.Map<IEnumerable<FoodConciseViewModel>>(meals);

            return View(orderModel);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var user = this.GetCurrentUser();
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            order.Approved = true;
            order.WaiterId = user.Id;
            await this.context.SaveChangesAsync();

            return RedirectToAction("OrdersForApproval", "Orders", new { area = "Employee" });
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

            return RedirectToAction("OrdersNotBeingCooked", "Orders", new { area = "Employee" });
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
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            if(order.DrinksAreFinished || order.OrderDrinks == null)
            {
                order.IsFinished = true;
            }
            order.IsBeingCooked = false;
            order.MealsAreFinished = true;
            await this.context.SaveChangesAsync();

            return RedirectToAction("TakenMealsOrders", "Orders", new { area = "Employee" });
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
            if(order.IsBeingCooked || order.DrinksAreBeingPrepped || (order.OrderDrinks != null && !order.DrinksAreFinished) || (order.OrderFoods != null && !order.MealsAreFinished))
            {
                TempData["finishedMessage"] = "Some things have not been delivered yet!";
                return RedirectToAction("FinishedOrders", "Orders", new { area = "Employee" });
            }
            order.TimeOfPayment = DateTime.Now;
            await this.context.SaveChangesAsync();

            return RedirectToAction("FinishedOrders", "Orders", new { area = "Employee" });
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

            return RedirectToAction("DrinksWithoutBartender", "Orders", new { area = "Employee" });
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
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            if (order.MealsAreFinished || order.OrderFoods == null)
            {
                order.IsFinished = true;
            }
            order.DrinksAreBeingPrepped = false;
            order.DrinksAreFinished = true;
            await this.context.SaveChangesAsync();

            return RedirectToAction("TakenDrinksOrders", "Orders", new { area = "Employee" });
        }

        private User GetCurrentUser()
        {
            var name = this.User.Identity.Name;
            var user = this.context.Users.FirstOrDefault(u => u.UserName == name);
            return user;
        }
    }
}