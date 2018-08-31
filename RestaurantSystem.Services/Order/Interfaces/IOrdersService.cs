using RestaurantSystem.Common.Employee.ViewModels;
using RestaurantSystem.Common.Order.ViewModels;
using RestaurantSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Order.Interfaces
{
    public interface IOrdersService
    {
        List<Food> GetMealsFromIds(int[] mealsIds);

        List<Drink> GetDrinksFromIds(int[] drinksIds);

        CartViewModel CreateCart(List<Food> meals, List<Drink> drinks);

        Task<int> MakeAnOrder(List<Food> meals, List<Drink> drinks, string[] Tables);

        OrderViewModel GetOrderDetailsById(int id);
    }
}