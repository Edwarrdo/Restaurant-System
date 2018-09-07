using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Common.Constants;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Storage.Interfaces;

namespace RestaurantSystem.Web.Pages.Menu
{
    public class OrderModel : PageModel
    {
        private IStorageService storageService;

        public OrderModel(IStorageService storageService)
        {
            this.storageService = storageService;
        }
        

        public IActionResult OnGetOrderFood(int id)
        {
            if(HttpContext.Session.GetString("meals") != null)
            {
                var meals = HttpContext.Session.GetString("meals");
                HttpContext.Session.SetString("meals", meals + "," + id.ToString());
            }
            else
            {
                HttpContext.Session.SetString("meals", id.ToString());
            }
            this.TempData[WebConstants.GoodMessage] = string.Format(Messages.AddToCartSuccessMessage, "Meal");

            var meal = this.storageService.GetMealById(id);
            //STUPID IDEA BUT WHO CARES IT WORKS!
            var category = meal.Category;
            return RedirectToPage(category + "s");
        }

        public IActionResult OnGetOrderDrink(int id)
        {
            if (HttpContext.Session.GetString("drinks") != null)
            {
                var drinks = HttpContext.Session.GetString("drinks");
                HttpContext.Session.SetString("drinks", drinks + "," + id.ToString());
            }
            else
            {
                HttpContext.Session.SetString("drinks", id.ToString());
            }
            this.TempData[WebConstants.GoodMessage] = string.Format(Messages.AddToCartSuccessMessage, typeof(Drink).Name);
            return RedirectToPage("Drinks");
        }

        public async Task<IActionResult> OnGetBanFood(int id)
        {
            var result = await this.storageService.BanMealById(id);
            if(result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.BanFailureMessage, typeof(Food).Name);
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.BanSuccessMessage, typeof(Food).Name);
            }
            var meal = this.storageService.GetMealById(id);
            //STUPID IDEA BUT WHO CARES IT WORKS!
            var category = meal.Category;
            return RedirectToPage(category + "s");
        }

        public async Task<IActionResult> OnGetBanDrink(int id)
        {
            var result = await this.storageService.BanDrinkById(id);
            if (result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.BanFailureMessage, typeof(Drink).Name);
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.BanSuccessMessage, typeof(Drink).Name);
            }
            return RedirectToPage("Drinks");
        }
    }
}