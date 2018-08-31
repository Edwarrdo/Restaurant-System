using RestaurantSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Storage.Interfaces
{
    public interface IStorageService
    {
        Food GetMealById(int id);

        IEnumerable<Drink> GetAllDrinks();

        IEnumerable<Food> GetAllMealsByCategory(string category);

        Task<int> BanMealById(int id);

        Task<int> BanDrinkById(int id);
    }
}
