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
    public class PorksModel : PageModel
    {
        private IStorageService storageService;

        public PorksModel(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public IEnumerable<Food> Porks { get; private set; }

        public void OnGet()
        {
            this.Porks = this.storageService.GetAllMealsByCategory("Pork").ToList();
        }
    }
}