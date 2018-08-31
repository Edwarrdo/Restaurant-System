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
    public class DrinksModel : PageModel
    {
        private IStorageService storageService;

        public DrinksModel(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public IEnumerable<Drink> Drinks { get; private set; }

        public void OnGet()
        {
            this.Drinks = this.storageService.GetAllDrinks().ToList();
        }
    }
}