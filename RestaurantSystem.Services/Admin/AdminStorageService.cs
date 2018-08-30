using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Admin.ViewModels;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Admin.Interfaces;

namespace RestaurantSystem.Services.Admin
{
    public class AdminStorageService : BaseEFService, IAdminStorageService
    {
        public AdminStorageService(RMSContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<int> AddProductAsync(ProductCreationBindingModel model)
        {
            try
            {
                var product = this.Mapper.Map<Product>(model);
                this.DbContext.Products.Add(product);
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

        public async Task<int> AddIngredientAsync(IngredientCreationBindingModel model)
        {
            try
            {
                var ingredient = this.Mapper.Map<Ingredient>(model);
                this.DbContext.Ingredients.Add(ingredient);
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

        public string[] GetAllProductsNames()
        {
            return DbContext.Products.Select(p => p.Name).ToArray();
        }

        public async Task<int> CreateMeal(MealBindingModel model)
        {
            try
            {
                var food = this.Mapper.Map<Food>(model);
                foreach (var product in model.Products)
                {
                    var productId = DbContext.Products.FirstOrDefault(p => p.Name == product).Id;
                    var foodProduct = new FoodProduct { FoodId = food.Id, ProductId = productId };
                    food.FoodProducts.Add(foodProduct);
                }

                this.DbContext.Foods.Add(food);
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

        public string[] GetAllIngredientsNames()
        {
            return DbContext.Ingredients.Select(i => i.Name).ToArray();
        }

        public async Task<int> CreateDrink(DrinkBindingModel model)
        {
            try
            {
                var drink = this.Mapper.Map<Drink>(model);
                foreach (var ingredient in model.Ingredients)
                {
                    var ingredientId = DbContext.Ingredients.FirstOrDefault(p => p.Name == ingredient).Id;
                    var drinkIngredient = new DrinkIngredient { DrinkId = drink.Id, IngredientId = ingredientId };
                    drink.DrinkIngredients.Add(drinkIngredient);
                }

                this.DbContext.Drinks.Add(drink);
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

        public IEnumerable<FoodConciseViewModel> GetAllMealsViewModels()
        {
            var foods = this.DbContext.Foods
                .ToArray();
            var userModels = this.Mapper.Map<IEnumerable<FoodConciseViewModel>>(foods);

            return userModels;
        }

        public IEnumerable<DrinkConciseViewModel> GetAllDrinksViewModels()
        {
            var foods = this.DbContext.Drinks
                .ToArray();
            var userModels = this.Mapper.Map<IEnumerable<DrinkConciseViewModel>>(foods);

            return userModels;
    }
    }
}
