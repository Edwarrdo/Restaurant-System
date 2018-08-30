using AutoMapper;
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
    public class WaiterService : BaseEFService, IWaiterService
    {
        public WaiterService(RMSContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<OrderConciseViewModel> GetOrdersForApproval()
        {
            var orders = this.DbContext.Orders.Where(o => o.Approved == false);
            var ordersModels = this.Mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return ordersModels;
        }

        public async Task<int> WaiterApproveOrder(string waiterName, int orderId)
        {
            try
            {
                var waiter = this.DbContext.Users.FirstOrDefault(u => u.UserName == waiterName);
                var order = this.DbContext.Orders.FirstOrDefault(o => o.Id == orderId);
                order.Approved = true;
                order.WaiterId = waiter.Id;
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

        public IEnumerable<OrderConciseViewModel> GetFinishedOrders()
        {
            var orders = this.DbContext.Orders.Where(o => o.IsFinished && o.TimeOfPayment == null);
            var ordersModels = this.Mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return ordersModels;
        }

        public async Task<int> PayForOrder(int id)
        {
            try
            {
                var order = this.DbContext.Orders.FirstOrDefault(o => o.Id == id);
                if (order.IsBeingCooked || order.DrinksAreBeingPrepped || (order.OrderDrinks != null && !order.DrinksAreFinished) || (order.OrderFoods != null && !order.MealsAreFinished))
                {
                    //not ready to be payed
                    return 0;
                }
                order.TimeOfPayment = DateTime.Now;
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
