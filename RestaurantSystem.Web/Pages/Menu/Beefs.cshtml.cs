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
    public class BeefsModel : PageModel
    {
        private IStorageService storageService;

        public BeefsModel(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public IEnumerable<Food> Beefs { get; private set; }

        public void OnGet()
        {
            this.Beefs = this.storageService.GetAllMealsByCategory("Beef").ToList();
        }
    }
}