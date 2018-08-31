using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Order.BindingModels;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Order.Interfaces;

namespace RestaurantSystem.Web.Controllers
{
    [AllowAnonymous]
    public class OrdersController : Controller
    {
        private IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult ShowCart()
        {
            if (HttpContext.Session.GetString("meals") == null && HttpContext.Session.GetString("drinks") == null)
            {
                return View();
            }
            var meals = GetOrderMealsFromSession();
            var drinks = GetOrderDrinksFromSession();
            var cart = this.ordersService.CreateCart(meals, drinks);
            return View(cart);
        }

        [HttpGet]
        public IActionResult DeliveryOrTable()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeliveryOrTable(DeliveryOrTableBindingModel model)
        {
            if (model.DeliveryOrTable == "Table")
            {
                return RedirectToAction("PickATable", "Orders", new { area = "" });
            }
            //TODO: implement delivery redirection
            return RedirectToAction("PickATable", "Orders", new { area = "" });
        }

        [HttpGet]
        public IActionResult PickATable()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAnOrder(TablesBindingModel model)
        {
            var meals = GetOrderMealsFromSession();
            var drinks = GetOrderDrinksFromSession();
            var result = await this.ordersService.MakeAnOrder(meals, drinks, model.Tables);
            if (result == 0)
            {
                this.TempData["badMessage"] = "Could not create the order!";
            }
            else
            {
                this.TempData["goodMessage"] = "Order created!";
            }
            this.HttpContext.Session.Clear();


            //TODO: make it less stupid
            if (this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            else if (this.User.IsInRole("Chef") || this.User.IsInRole("Waiter") || this.User.IsInRole("Bartender"))
            {
                return RedirectToAction("Index", "Home", new { area = "Employee" });
            }
            return RedirectToAction("Index", "Home", new { area = "Client" });
        }

        public IActionResult ClearCart()
        {
            this.HttpContext.Session.Clear();
            return RedirectToAction("ShowCart", "Orders", new { area = "" });
        }

        private List<Food> GetOrderMealsFromSession()
        {
            var mealsIds = new int[] { };
            if (HttpContext.Session.Keys.Contains("meals"))
            {
                mealsIds = HttpContext.Session.GetString("meals").Split(",").Select(int.Parse).ToArray();
            }
            else
            {
                return null;
            }
            var meals = this.ordersService.GetMealsFromIds(mealsIds);
            return meals;
        }

        private List<Drink> GetOrderDrinksFromSession()
        {
            var drinkIds = new int[] { };
            if (HttpContext.Session.Keys.Contains("drinks"))
            {
                drinkIds = HttpContext.Session.GetString("drinks").Split(",").Select(int.Parse).ToArray();
            }
            else
            {
                return null;
            }
            var drinks = this.ordersService.GetDrinksFromIds(drinkIds);
            return drinks;
        }

    }
}