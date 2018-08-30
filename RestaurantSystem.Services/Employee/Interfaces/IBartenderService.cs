using RestaurantSystem.Common.Employee.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Employee.Interfaces
{
    public interface IBartenderService
    {
        IEnumerable<OrderConciseViewModel> GetDrinksWithoutBartender();

        Task<int> TakeOrderAsync(int id);

        IEnumerable<OrderConciseViewModel> GetDrinksTakenByBartender();

        Task<int> FinishOrderAsync(int id);
    }
}
