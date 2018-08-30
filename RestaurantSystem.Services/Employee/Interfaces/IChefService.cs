using RestaurantSystem.Common.Employee.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Employee.Interfaces
{
    public interface IChefService
    {
        IEnumerable<OrderConciseViewModel> GetMealsNotBeingCooked();

        Task<int> TakeOrderAsync(int id, string ChefName);

        IEnumerable<OrderConciseViewModel> MealsTakenByChef(string chefName);

        Task<int> FinishOrderAsync(int id);
    }
}
