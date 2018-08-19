using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Admin.Models.BindingModels;

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
        public IActionResult AddDrink()
        {
            return View();
        }
    }
}