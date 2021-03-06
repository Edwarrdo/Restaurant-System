﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Constants;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Admin.Interfaces;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class StorageController : AdminController
    {
        private IAdminStorageService storageService;

        public StorageController(IAdminStorageService storageService)
        {
            this.storageService = storageService;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductCreationBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var result = await this.storageService.AddProductAsync(model);
            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.AddingFailureMessage, typeof(Product).Name, model.Name);
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.AddingSuccessMessage, typeof(Product).Name, model.Name);
            }
            return RedirectToAction("AddProduct", "Storage", new { Area = WebConstants.AdminArea });
        }

        [HttpGet]
        public IActionResult AddIngredient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddIngredient(IngredientCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await this.storageService.AddIngredientAsync(model);
            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.AddingFailureMessage, typeof(Ingredient).Name, model.Name);
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.AddingSuccessMessage, typeof(Ingredient).Name, model.Name);
            }
            return RedirectToAction("AddIngredient", "Storage", new { Area = WebConstants.AdminArea });
        }

        [HttpGet]
        public IActionResult AddMeal()
        {
            var products = this.storageService.GetAllProductsNames();
            this.ViewBag.Products = products;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeal(MealBindingModel model)
        {
            if(!this.ModelState.IsValid)
            {
                var products = this.storageService.GetAllProductsNames();
                this.ViewBag.Products = products;
                return this.View();
            }
            var result = await this.storageService.CreateMeal(model);

            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.AddingFailureMessage, "Meal", model.Name);
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.AddingSuccessMessage, "Meal", model.Name);
            }
            return RedirectToAction("AddMeal", "Storage", new { Area = WebConstants.AdminArea });
        }

        [HttpGet]
        public IActionResult AddDrink()
        {
            var ingredients = this.storageService.GetAllIngredientsNames();
            this.ViewBag.Ingredients = ingredients;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDrink(DrinkBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var ingredients = this.storageService.GetAllIngredientsNames();
                this.ViewBag.Ingredients = ingredients;
                return this.View();
            }
            var result = await this.storageService.CreateDrink(model);

            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.AddingFailureMessage, typeof(Drink).Name, model.Name);
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.AddingSuccessMessage, typeof(Drink).Name, model.Name);
            }
            return RedirectToAction("AddDrink", "Storage", new { Area = WebConstants.AdminArea });
        }
        [HttpGet]
        public IActionResult AllMeals()
        {
            var userModels = this.storageService.GetAllMealsViewModels();
            ViewBag.categories = WebConstants.MealsCategories;
            return View(userModels);
        }

        [HttpGet]
        public IActionResult AllDrinks()
        {
            var userModels = this.storageService.GetAllDrinksViewModels();
            return View(userModels);
        }
    }
}