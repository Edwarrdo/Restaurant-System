using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Common.Employee.ViewModels;
using RestaurantSystem.Data;
using RestaurantSystem.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Employee
{
    public class BartenderService : BaseEFService, IBartenderService
    {
        public BartenderService(RMSContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<OrderConciseViewModel> GetDrinksWithoutBartender()
        {
            var orders = this.DbContext.Orders.Where(o => o.Approved && !o.DrinksAreFinished && !o.DrinksAreBeingPrepped && o.OrderDrinks != null && o.OrderDrinks.Any());
            var ordersModels = this.Mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return ordersModels;
        }

        public async Task<int> TakeOrderAsync(int id)
        {
            try
            {
                var order = this.DbContext.Orders.FirstOrDefault(o => o.Id == id);
                order.DrinksAreBeingPrepped = true;
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

        public IEnumerable<OrderConciseViewModel> GetDrinksTakenByBartender()
        {
            var orders = this.DbContext.Orders.Where(o => o.DrinksAreBeingPrepped);
            var ordersModels = this.Mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return ordersModels;
        }

        public async Task<int> FinishOrderAsync(int id)
        {
            try
            {
                var order = this.DbContext.Orders.Include(o => o.OrderFoods).FirstOrDefault(o => o.Id == id);
                if (order.MealsAreFinished || order.OrderFoods == null)
                {
                    order.IsFinished = true;
                }
                order.DrinksAreBeingPrepped = false;
                order.DrinksAreFinished = true;
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
