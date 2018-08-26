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
    public class ChickensModel : PageModel
    {
        private RMSContext context;

        public ChickensModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Chickens { get; private set; }

        public void OnGet()
        {
            this.Chickens = this.context.Foods.Where(f => f.Category == "Chicken").ToList();
        }
    }
}