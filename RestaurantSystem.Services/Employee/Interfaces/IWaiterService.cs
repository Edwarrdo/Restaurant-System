using RestaurantSystem.Common.Employee.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Employee.Interfaces
{
    public interface IWaiterService
    {
        IEnumerable<OrderConciseViewModel> GetOrdersForApproval();

        Task<int> WaiterApproveOrder(string waiterName, int orderId);

        IEnumerable<OrderConciseViewModel> GetFinishedOrders();

        Task<int> PayForOrder(int id);
    }
}
