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
    public class AppetizersModel : PageModel
    {
        private RMSContext context;

        public AppetizersModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Appetizers { get; private set; }

        public void OnGet()
        {
            this.Appetizers = this.context.Foods.Where(f => f.Category == "Appetizer").ToList();
        }
    }
}