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
    public class DessertsModel : PageModel
    {
        private IStorageService storageService;

        public DessertsModel(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public IEnumerable<Food> Desserts { get; private set; }

        public void OnGet()
        {
            this.Desserts = this.storageService.GetAllMealsByCategory("Dessert").ToList();
        }
    }
}