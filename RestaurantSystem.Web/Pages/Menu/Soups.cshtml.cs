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
    public class SoupsModel : PageModel
    {
        private IStorageService storageService;

        public SoupsModel(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public IEnumerable<Food> Soups { get; private set; }

        public void OnGet()
        {
            this.Soups = this.storageService.GetAllMealsByCategory("Soup").ToList();
        }
    }
}