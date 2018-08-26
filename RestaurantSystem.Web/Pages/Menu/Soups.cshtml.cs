using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Web.Pages.Menu
{
    public class SoupsModel : PageModel
    {
        private RMSContext context;

        public SoupsModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Soups { get; private set; }

        public void OnGet()
        {
            this.Soups = this.context.Foods.Where(f => f.Category == "Soup").ToList();
        }
    }
}