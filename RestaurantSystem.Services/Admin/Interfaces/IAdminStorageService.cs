using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Admin.Interfaces
{
    public interface IAdminStorageService
    {
        Task<int> AddProductAsync(ProductCreationBindingModel model);

        Task<int> AddIngredientAsync(IngredientCreationBindingModel model);

        string[] GetAllProductsNames();

        Task<int> CreateMeal(MealBindingModel model);

        string[] GetAllIngredientsNames();

        Task<int> CreateDrink(DrinkBindingModel model);

        IEnumerable<FoodConciseViewModel> GetAllMealsViewModels();

        IEnumerable<DrinkConciseViewModel> GetAllDrinksViewModels();
    }
}
