using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Common.Employee.ViewModels;
using RestaurantSystem.Common.Order.ViewModels;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Exceptions;
using RestaurantSystem.Services.Order.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Order
{
    public class OrdersService : BaseEFService, IOrdersService
    {
        public OrdersService(RMSContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public List<Food> GetMealsFromIds(int[] mealsIds)
        {
            var meals = new List<Food>();
            foreach (var id in mealsIds)
            {
                var meal = this.DbContext.Foods.FirstOrDefault(f => f.Id == id);
                meals.Add(meal);
            }
            return meals;
        }

        public List<Drink> GetDrinksFromIds(int[] drinksIds)
        {
            var drinks = new List<Drink>();
            foreach (var id in drinksIds)
            {
                var drink = this.DbContext.Drinks.FirstOrDefault(f => f.Id == id);
                drinks.Add(drink);
            }
            return drinks;
        }

        public CartViewModel CreateCart(List<Food> meals, List<Drink> drinks)
        {
            var mealsModel = this.Mapper.Map<IEnumerable<MealConciseViewModel>>(meals);
            var drinksModel = this.Mapper.Map<IEnumerable<Common.Order.ViewModels.DrinkConciseViewModel>>(drinks);
            var model = new CartViewModel()
            {
                Drinks = drinksModel,
                Meals = mealsModel
            };
            model.Price = 0;
            if (drinks != null)
            {
                model.Price += drinksModel.Sum(dm => dm.Price);
            }
            if (meals != null)
            {
                model.Price += mealsModel.Sum(md => md.Price);
            }

            return model;
        }

        public async Task<int> MakeAnOrder(List<Food> meals, List<Drink> drinks, string[] Tables)
        {
            try
            {
                var order = new Models.Order();
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
                order.TableNumbers = string.Join(",", Tables);
                order.Price = meals.Sum(m => m.Price);
                order.TimeOfOrder = DateTime.Now;
                this.DbContext.Orders.Add(order);
                await this.DbContext.SaveChangesAsync();
            }
            catch
            {
                //failure
                return 0;
            }
            //success
            return 1;
        }

        public OrderViewModel GetOrderDetailsById(int id)
        {
            var order = this.DbContext.Orders.Include(o => o.OrderFoods).Include(o => o.OrderDrinks).FirstOrDefault(o => o.Id == id);
            if(order == null)
            {
                throw new NotFoundException();
            }
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
                    var drink = this.DbContext.Drinks.FirstOrDefault(d => d.Id == od.DrinkId);
                    drinks.Add(drink);
                }
            }
            if (order.OrderFoods != null)
            {
                foreach (var of in order.OrderFoods)
                {
                    var food = this.DbContext.Foods.FirstOrDefault(f => f.Id == of.FoodId);
                    meals.Add(food);
                }
            }

            orderModel.Drinks = this.Mapper.Map<IEnumerable<Common.Employee.ViewModels.DrinkConciseViewModel>>(drinks);
            orderModel.Meals = this.Mapper.Map<IEnumerable<FoodConciseViewModel>>(meals);

            return orderModel;
        }
    }
}