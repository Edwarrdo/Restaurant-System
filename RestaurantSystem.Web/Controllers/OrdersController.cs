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

        public IActionResult MakeAnOrder()
        {
            var meals = GetOrderMealsFromSession();
            var order = new Order();
            order.OrderFoods = meals.Select(m => new OrderFood
            {
                FoodId = m.Id,
                OrderId = order.Id
            });

            order.IsFinished = false;
            order.Price = meals.Sum(m => m.Price);
            order.TimeOfOrder = DateTime.Now;

            return View();
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