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
    public class DrinksModel : PageModel
    {
        private RMSContext context;

        public DrinksModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Drink> Drinks { get; private set; }

        public void OnGet()
        {
            this.Drinks = this.context.Drinks.ToList();
        }
    }
}