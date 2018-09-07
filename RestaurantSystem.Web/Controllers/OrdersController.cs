using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Constants;
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
            if (HttpContext.Session.GetString(WebConstants.MealsSessionKey) == null && HttpContext.Session.GetString(WebConstants.DrinksSessionKey) == null)
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
                this.TempData[WebConstants.BadMessage] = Messages.OrderCreationFailureMessage;
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = Messages.OrderCreationSuccessMessage;
            }
            this.HttpContext.Session.Clear();


            //TODO: make it less stupid
            if (this.User.IsInRole(WebConstants.AdminRole))
            {
                return RedirectToAction("Index", "Home", new { area = WebConstants.AdminArea });

            }
            else if (this.User.IsInRole(WebConstants.ChefRole) || this.User.IsInRole(WebConstants.WaiterRole) || this.User.IsInRole(WebConstants.BartenderRole))
            {
                return RedirectToAction("Index", "Home", new { area = WebConstants.EmployeeArea });
            }
            return RedirectToAction("Index", "Home", new { area = WebConstants.ClientArea });
        }

        public IActionResult ClearCart()
        {
            this.HttpContext.Session.Clear();
            return RedirectToAction("ShowCart", "Orders", new { area = "" });
        }

        private List<Food> GetOrderMealsFromSession()
        {
            var mealsIds = new int[] { };
            if (HttpContext.Session.Keys.Contains(WebConstants.MealsSessionKey))
            {
                mealsIds = HttpContext.Session.GetString(WebConstants.MealsSessionKey).Split(",").Select(int.Parse).ToArray();
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
            if (HttpContext.Session.Keys.Contains(WebConstants.DrinksSessionKey))
            {
                drinkIds = HttpContext.Session.GetString(WebConstants.DrinksSessionKey).Split(",").Select(int.Parse).ToArray();
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