using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Storage.Interfaces;

namespace RestaurantSystem.Web.Pages.Menu
{
    public class ChickensModel : PageModel
    {
        private IStorageService storageService;

        public ChickensModel(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public IEnumerable<Food> Chickens { get; private set; }

        public void OnGet()
        {
            this.Chickens = this.storageService.GetAllMealsByCategory("Chicken").ToList();
        }
    }
}