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
            var mealsModel = this.mapper.Map<IEnumerable<MealConciseViewModel>>(meals);
            return View(mealsModel);
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

        public async Task<IActionResult> MakeAnOrder(TablesBindingModel model)
        {
            var meals = GetOrderMealsFromSession();
            var order = new Order();
            order.OrderFoods = new List<OrderFood> (meals.Select(m => new OrderFood
            {
                FoodId = m.Id,
                OrderId = order.Id
            }));
            order.TableNumbers = model.Tables[0];
            order.IsFinished = false;
            order.Price = meals.Sum(m => m.Price);
            order.TimeOfOrder = DateTime.Now;
            this.context.Orders.Add(order);
            await this.context.SaveChangesAsync();
            this.HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home", new { area = "Client" });
        }



        private List<Food> GetOrderMealsFromSession()
        {
            var orderIds = HttpContext.Session.GetString("meals").Split(",").Select(int.Parse).ToArray();
            var meals = new List<Food>();
            foreach (var id in orderIds)
            {
                var meal = this.context.Foods.FirstOrDefault(f => f.Id == id);
                meals.Add(meal);
            }
            return meals;
        }
    }
}