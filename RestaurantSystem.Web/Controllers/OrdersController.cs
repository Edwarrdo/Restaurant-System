using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Models.BindingModels;
using RestaurantSystem.Web.Models.ViewModels;

namespace RestaurantSystem.Web.Controllers
{
    [AllowAnonymous]
    public class OrdersController : Controller
    {
        private RMSContext context;
        private IMapper mapper;

        public OrdersController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult ShowCart()
        {
            if (HttpContext.Session.GetString("meals") == null)
            {
                return View();
            }
            var meals = GetOrderMealsFromSession();
            var drinks = GetOrderDrinksFromSession();
            var mealsModel = this.mapper.Map<IEnumerable<MealConciseViewModel>>(meals);
            var drinksModel = this.mapper.Map<IEnumerable<DrinkConciseViewModel>>(drinks);
            var model = new CartViewModel()
            {
                Drinks = drinksModel,
                Meals = mealsModel
            };
            model.Price = 0;
            if(drinks != null)
            {
                model.Price += drinksModel.Sum(dm => dm.Price);
            }
            if(meals != null)
            {
                model.Price += mealsModel.Sum(md => md.Price);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeliveryOrTable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeliveryOrTable(DeliveryOrTableBindingModel model)
        {
            if(model.DeliveryOrTable == "Table")
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
        public async Task<IActionResult> MakeAnOrder(TablesBindingModel model)
        {
            var meals = GetOrderMealsFromSession();
            var drinks = GetOrderDrinksFromSession();
            var order = new Order();
            if (meals != null)
            {
                order.OrderFoods = new List<OrderFood>(meals.Select(m => new OrderFood
                {
                    FoodId = m.Id,
                    OrderId = order.Id
                }));
            }
            if (drinks != null)
            {
                order.OrderDrinks = new List<OrderDrink>(drinks.Select(d => new OrderDrink
                {
                    DrinkId = d.Id,
                    OrderId = order.Id
                }));
            }
            order.TableNumbers = string.Join(",", model.Tables);
            //i know the default value of bool is false, i assign it for other reasons!
            order.IsFinished = false;
            order.MealsAreFinished = false;
            order.DrinksAreFinished = false;
            order.DrinksAreBeingPrepped = false;
            order.IsBeingCooked = false;
            order.Price = meals.Sum(m => m.Price);
            order.TimeOfOrder = DateTime.Now;
            this.context.Orders.Add(order);
            await this.context.SaveChangesAsync();
            this.HttpContext.Session.Clear();

            var currUser = this.GetCurrentUser();

            //TODO: make it less stupid
            if(currUser.IsEmployee)
            {
                return RedirectToAction("Index", "Home", new { area = "Employee" });
            }
            else if(currUser.UserName == "admin")
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Home", new { area = "Client" });
        }



        private List<Food> GetOrderMealsFromSession()
        {
            var orderIds = new int[] { };
            if (HttpContext.Session.Keys.Contains("meals"))
            {
                orderIds = HttpContext.Session.GetString("meals").Split(",").Select(int.Parse).ToArray();
            }
            else
            {
                return null;
            }
            var meals = new List<Food>();
            foreach (var id in orderIds)
            {
                var meal = this.context.Foods.FirstOrDefault(f => f.Id == id);
                meals.Add(meal);
            }
            return meals;
        }

        private List<Drink> GetOrderDrinksFromSession()
        {
            var orderIds = new int[] { };
            if (HttpContext.Session.Keys.Contains("drinks"))
            {
                orderIds = HttpContext.Session.GetString("drinks").Split(",").Select(int.Parse).ToArray();
            }
            else
            {
                return null;
            }
            var drinks = new List<Drink>();
            foreach (var id in orderIds)
            {
                var drink = this.context.Drinks.FirstOrDefault(f => f.Id == id);
                drinks.Add(drink);
            }
            return drinks;
        }

        private User GetCurrentUser()
        {
            var name = this.User.Identity.Name;
            var user = this.context.Users.FirstOrDefault(u => u.UserName == name);
            return user;
        }
    }
}