using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Admin.BindingModels;
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
                this.TempData["badMessage"] = $"Product {model.Name} coultn't be added!";
            }
            else
            {
                this.TempData["goodMessage"] = $"Product {model.Name} successfully added!";
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
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
                this.TempData["badMessage"] = $"Ingredient {model.Name} coultn't be added!";
            }
            else
            {
                this.TempData["goodMessage"] = $"Ingredient {model.Name} successfully added!";
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
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
                this.TempData["badMessage"] = $"Meal {model.Name} coultn't be added!";
            }
            else
            {
                this.TempData["goodMessage"] = $"Meal {model.Name} successfully added!";
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
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
                this.TempData["badMessage"] = $"Drink {model.Name} coultn't be added!";
            }
            else
            {
                this.TempData["goodMessage"] = $"Drink {model.Name} successfully added!";
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult AllMeals()
        {
            var userModels = this.storageService.GetAllMealsViewModels();
            ViewBag.categories = new string[] { "Salat", "Appetizer", "Pizza", "Pasta", "Risotto", "Chicken", "Pork", "Beef", "Soup", "Dessert" };
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