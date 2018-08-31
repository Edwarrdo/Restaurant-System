using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Common.Employee.ViewModels;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Employee
{
    public class ChefService : BaseEFService, IChefService
    {
        public ChefService(RMSContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<OrderConciseViewModel> GetMealsNotBeingCooked()
        {
            var orders = this.DbContext.Orders.Where(o => o.Approved && !o.IsBeingCooked && !o.MealsAreFinished && o.OrderFoods != null);
            var ordersModel = this.Mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);

            return ordersModel;
        }

        public async Task<int> TakeOrderAsync(int id, string ChefName)
        {
            try
            {
                var order = this.DbContext.Orders.FirstOrDefault(o => o.Id == id);
                var user = this.DbContext.Users.FirstOrDefault(u => u.UserName == ChefName);
                order.ChefId = user.Id;
                order.IsBeingCooked = true;
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

        public IEnumerable<OrderConciseViewModel> MealsTakenByChef(string chefName)
        {
            var chef = this.DbContext.Users.FirstOrDefault(u => u.UserName == chefName);
            var orders = this.DbContext.Orders.Where(o => o.Approved == true && o.IsBeingCooked == true && o.ChefId == chef.Id);
            var ordersModels = this.Mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return ordersModels;
        }

        public async Task<int> FinishOrderAsync(int id)
        {
            try
            {
                var order = this.DbContext.Orders.Include(o => o.OrderDrinks).FirstOrDefault(o => o.Id == id);
                if (order.DrinksAreFinished || order.OrderDrinks == null || order.OrderDrinks.Count() == 0)
                {
                    order.IsFinished = true;
                }
                order.IsBeingCooked = false;
                order.MealsAreFinished = true;
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
    }
}
