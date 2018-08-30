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
    }
}