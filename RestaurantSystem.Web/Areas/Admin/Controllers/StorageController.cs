using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Admin.Models.BindingModels;
using RestaurantSystem.Web.Areas.Admin.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class StorageController : AdminController
    {
        private RMSContext context;
        private IMapper mapper;

        public StorageController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreationBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var product = this.mapper.Map<Product>(model);
            this.context.Products.Add(product);
            await this.context.SaveChangesAsync();
            this.TempData["message"] = $"Product {product.Name} successfully added!";
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult AddIngredient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(IngredientCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var ingredient = this.mapper.Map<Ingredient>(model);
            this.context.Ingredients.Add(ingredient);
            await this.context.SaveChangesAsync();
            this.TempData["message"] = $"Ingredient {ingredient.Name} successfully added!";
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult AddMeal()
        {
            var products = context.Products.Select(p => p.Name).ToArray();
            this.ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMeal(MealBindingModel model)
        {
            if(!this.ModelState.IsValid)
            {
                var products = context.Products.Select(p => p.Name).ToArray();
                this.ViewBag.Products = products;
                return this.View();
            }
            var food = this.mapper.Map<Food>(model);
            foreach (var product in model.Products)
            {
                var productId = context.Products.FirstOrDefault(p => p.Name == product).Id;
                var foodProduct = new FoodProduct { FoodId = food.Id, ProductId = productId};
                food.FoodProducts.Add(foodProduct);
            }

            this.context.Foods.Add(food);
            await this.context.SaveChangesAsync();
            this.TempData["message"] = $"Food {food.Name} successfully added!";
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult AddDrink()
        {
            var ingredients = context.Ingredients.Select(i => i.Name).ToArray();
            this.ViewBag.Ingredients = ingredients;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDrink(DrinkBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var ingredients = context.Ingredients.Select(i => i.Name).ToArray();
                this.ViewBag.Ingredients = ingredients;
                return this.View();
            }
            var drink = this.mapper.Map<Drink>(model);
            foreach (var ingredient in model.Ingredients)
            {
                var ingredientId = context.Ingredients.FirstOrDefault(p => p.Name == ingredient).Id;
                var drinkIngredient = new DrinkIngredient { DrinkId = drink.Id, IngredientId = ingredientId };
                drink.DrinkIngredients.Add(drinkIngredient);
            }

            this.context.Drinks.Add(drink);
            await this.context.SaveChangesAsync();
            this.TempData["message"] = $"Drink {drink.Name} successfully added!";
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult Menu()
        {
            var foods = this.context.Foods
                .ToArray();
            var userModels = this.mapper.Map<IEnumerable<FoodConciseViewModel>>(foods);

            return View(userModels);
        }
    }
}